using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using TA_PROJECT_CyclecountAPI.Model.API.Lx17;
using TA_PROJECT_CyclecountAPI.Model.LICC;
using TA_PROJECT_CyclecountAPI.Model.Lx17;
using TA_PROJECT_CyclecountAPI.ViewModel;

namespace TA_PROJECT_CyclecountAPI.DAL.Services
{
    public interface ILx17Service
    {
        Task<PaginationModel<Lx17>> GetLx17(string plant, Lx17MaterialFilterModel filter);
        Task<PaginationModel<Lx17>> GetRecountLx17(string plant, Lx17MaterialFilterModel filter);
        Task<PaginationModel<Lx17>> GetCountableLx17(string plant, Lx17MaterialFilterModel filter);
        Task<List<Lx17>> CreateInventory(List<Lx17> Model);
        Task<List<Lx17>> LICCToLx17(List<LICC> Liccs);
        Task<Lx17SAPRes> FetchLx17SAP(Lx17SAPReq req);
        Task<List<Lx17>> UpdateLx17(List<Lx17> data);
        Task<List<Lx17>> CountIvnumLx17(List<Lx17> req);
        Task<List<Lx17>> ReCountIvnumLx17(List<Lx17> req);
        Task<List<Lx17>> ToggleRecountLx17(List<Lx17> Lx17);
    }
    public class Lx17Service : ILx17Service
    {
        private readonly HttpClient client;
        private readonly CyclecountContext context;
        private readonly ClaimsPrincipal User;
        private readonly ILx17LogService logService;
        private readonly IMapper mapper;
        public Lx17Service(IHttpClientFactory factory, CyclecountContext context,IHttpContextAccessor httpContext, ILx17LogService logService,IMapper mapper)
        {
            client = factory.CreateClient("SAP_API");
            this.context = context;
            User = httpContext.HttpContext!.User;
            this.logService = logService;
            this.mapper = mapper;
        }

        public async Task<PaginationModel<Lx17>> GetLx17(string plant,Lx17MaterialFilterModel filter)
        {
            var Query = context.Lx17.Include(x => x.Plant).Where(x => x.Plant!.WERKS == plant &&
            (
                (x.Matnr.Contains(filter.Search ?? "")  || x.Ivnum.Contains(filter.Search??"") || x.Lgpla.Contains(filter.Search ?? "") || filter.Search == "")
                 &&
                (x.Lgtyp.Contains(filter.Lgtyp ?? "") || filter.Lgtyp == "" )
                &&
                (x.Istat == "Z")&&
                (filter.dFrom <= x.Ymd8up && filter.dTo >= x.Ymd8up)
            ));
            var ttl = await Query.CountAsync();
            return new PaginationModel<Lx17>()
            {
                Data = await Query.Skip((filter.page - 1) * filter.pagesize).Take(filter.pagesize).ToListAsync(),
                firstPage = filter.page == 1,
                lastPage = filter.page == Math.Ceiling((decimal)(ttl / filter.pagesize)),
                PageNum = filter.page,
                TotalData = ttl,
            };
        }
        public async Task<List<Lx17>> CreateInventory(List<Lx17> Model)
        {
            InvCreatemodel request = new InvCreatemodel(User.FindFirstValue(ClaimTypes.NameIdentifier),Model.First().Lgtyp,Model.First().Lgnum!.ToString() ?? "");
            List<Lx17> Res = new List<Lx17>();
            request.T_LAGP.item = new InvCreatemodel.T_LAGPs.items_T_LAGPs[Model.Count];
            request.T_LQUA.item = new InvCreatemodel.T_LQUAs.items_T_LQUAs[Model.Count];
            for (int i=0;i<Model.Count;i++)
            {
                request.T_LAGP.item[i] = new InvCreatemodel.T_LAGPs.items_T_LAGPs() { LGNUM = Model[i].Lgnum.ToString() ?? "", LGPLA = Model[i].Lgpla, LGTYP = Model[i].Lgtyp };
                request.T_LQUA.item[i] = new InvCreatemodel.T_LQUAs.items_T_LQUAs() { LGNUM = Model[i].Lgnum.ToString() ?? "", LQNUM = Model[i].Lqnum };
            }
            using (client)
            {
                var cnt = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var res = await client.PostAsync("RESTAdapter/REST/INV_DOC_CREATE", cnt);
                var json = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                    throw new Exception(json);
                if (Model.Count > 1)
                {
                    var data = JsonConvert.DeserializeObject<InvCreateResponse>(json);
                    if (data is null)
                        throw new Exception(json);

                    for (int i = 0; i < data.Record.T_LINV.item_T_LINV.Length; i++)
                    {
                        var item = data.Record.T_LINV.item_T_LINV[i];
                        var find = Model.Where(x => x.Matnr == item.Matnr && x.Lgpla == item.Lgpla && x.Lgtyp == item.Lgtyp && x.Lqnum == item.Lqnum).FirstOrDefault();
                        if (find is null)
                            continue;
                        item.Id = find.Id;
                        item.FromLICC = true;
                        item.Cntstatus = "Not Counted";
                        item.Ymd8up = DateTime.Now;
                        item.Plant = null;
                        item.PlantID = find.PlantID;
                        item.Idusin = find.Idusin;
                        item.Plant = null;
                        item.Idusup = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
                        context.Entry(find).CurrentValues.SetValues(item);
                        await context.SaveChangesAsync();
                        Res.Add(item);
                        await logService.RegisterInventory(item);
                        await RemoveLicc(item);
                    }
                }
                else
                {
                    var data = JsonConvert.DeserializeObject<InvCreateResponseSingle>(json);
                    if (data is null)
                        throw new Exception(json);
                    var item = data.Record.T_LINV.item_T_LINV;
                    var find = Model.Where(x => x.Matnr == item.Matnr && x.Lgpla == item.Lgpla && x.Lgtyp == item.Lgtyp && x.Lqnum == item.Lqnum).FirstOrDefault();
                    if (find is null)
                        throw new Exception("Item Not Found");
                    item.Id = find.Id;
                    item.FromLICC = true;
                    item.Plant = null;
                    item.PlantID = find.PlantID;
                    item.Idusin = find.Idusin;
                    item.Ymd8up = DateTime.Now;
                    item.Idusup = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
                    context.Entry(find).CurrentValues.SetValues(item);
                    await context.SaveChangesAsync();
                    Res.Add(item);
                    await logService.RegisterInventory(item);
                    await RemoveLicc(item);
                }
            }
            return Res;
        }
        private async Task RemoveLicc(Lx17 mat)
        {
            var find = await context.LICC.Where(x => x.LGPLA == mat.Lgpla && x.MATNR == mat.Matnr && x.LGTYP == mat.Lgtyp && x.WERKS == mat.Werks && x.LGNUM == mat.Lgnum.ToString()).FirstOrDefaultAsync();
            if (find is null)
                return;
            context.LICC.Remove(find);
            await context.SaveChangesAsync();
        }

        public async Task<List<Lx17>> LICCToLx17(List<LICC> Liccs)
        {
            List<Lx17> lx17 = mapper.Map<List<Lx17>>(Liccs);
            for (int i = 0; i < lx17.Count; i++)
            {
                lx17[i].Id = null;
                lx17[i].InsertedBy= null;
                lx17[i].Counter = null;
                lx17[i].Plant = null;
                Liccs[i].Plant = null;
                Liccs[i].InsertedBy = null;
                Liccs[i].UpdatedBy = null;
            }
            await context.AddRangeAsync(lx17);
            await context.SaveChangesAsync();
            context.RemoveRange(Liccs);
            await context.SaveChangesAsync();
            return lx17;
        }

        public async Task<Lx17SAPRes> FetchLx17SAP(Lx17SAPReq req)
        {
            Lx17SAPRes response = new Lx17SAPRes();
            using (client)
            {
                var content = new StringContent(JsonConvert.SerializeObject(req),Encoding.UTF8,"application/json");
                var res = await client.PostAsync("RESTAdapter/REST/INV_DIFF_LIST",content);
                var json = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                    throw new Exception(json);
                response = JsonConvert.DeserializeObject<Lx17SAPRes>(json);
            }
            return response;
        }

        public async Task<Lx17> FetchLx17SAP(Lx17 lx)
        {
            Lx17? lx17 = new Lx17();
            Lx17SAPReq? req = new Lx17SAPReq() { IM_LGNUM = ((int)lx.Lgnum!).ToString(), IM_NIGEZ = "x" };
            Lx17SAPRes? response = new Lx17SAPRes();
            using (client)
            {
                var content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                var res = await client.PostAsync("RESTAdapter/REST/INV_DIFF_LIST", content);
                var json = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                    throw new Exception(json);
                response = JsonConvert.DeserializeObject<Lx17SAPRes>(json);
                if (response == null)
                    throw new Exception(json);
                lx17 = response.ET_LIST.item.Where(x => x.Matnr == lx.Matnr && x.Lgpla == lx.Matnr && x.Lgtyp == lx.Lgtyp && x.Ivnum == lx.Ivnum && x.Lqnum == lx.Lqnum).FirstOrDefault();
            }
            if (lx is null)
                throw new Exception("Lx17 not Found");
            return lx;
        }

        public async Task<List<Lx17>> UpdateLx17(List<Lx17> data)
        {
            var id = User.FindFirstValue(ClaimTypes.Sid);
            data = data.Where(x => x.Cntstatus != "Recount").ToList();
            List<int> UpdatedId = new List<int>();
            for (int i=0;i<data.Count;i++)
            {
                Lx17 item = data[i];
                int? lx17id = null;
                var find = await context.Lx17.Where(x => x.Ivnum == item.Ivnum && x.Matnr == item.Matnr && x.Lgpla == item.Lgpla && x.Lgtyp == item.Lgtyp && x.Lqnum == item.Lqnum).FirstOrDefaultAsync();
                if (find is null)
                {

                    var plant = await context.Depts.Where(x => x.WERKS == item.Werks).FirstOrDefaultAsync();
                    if (plant is null)
                    {
                        Console.Error.WriteLine(JsonConvert.SerializeObject(item));
                        continue;
                    }
                    item.PlantID = plant.Id;
                    item.Idusup = int.Parse(id);
                    item.Ymd8up = DateTime.Now;
                    item.Idusin = int.Parse(id);
                    item.Ymd8in = DateTime.Now;
                    item.Cntstatus = "Not Counted";
                    item.FromLICC = true;
                    var s = await context.Lx17.AddAsync(item);
                    await context.SaveChangesAsync();
                    lx17id = s.Entity.Id;
                }
                else if (find is not null && find.Cntstatus == "Recount")
                {
                    UpdatedId.Add((int)find.Id);
                    continue;
                }
                else
                {
                    lx17id = item.Id = find.Id;
                    item.Abcin = find.Abcin;
                    item.Idusup = int.Parse(id);
                    item.Ymd8up = find.Ymd8up;
                    item.PlantID = find.PlantID;
                    item.Cntstatus = item.Istat == "Z" ? "Counted" : "Not Counted";
                    item.FromLICC = find.FromLICC;
                    item.Idusin = find.Idusin;
                    item.Ymd8in = find.Ymd8in;
                    context.Entry(find).CurrentValues.SetValues(item);
                    await context.SaveChangesAsync();
                }
                if (lx17id is not null)
                    UpdatedId.Add((int)lx17id);
            }
            string q = string.Join(",", UpdatedId);
            string Query= string.Format("Update Lx17 set CntStatus='Not Exists In SAP' Where Id not IN ({0}) and istat!='Z' and CntStatus !='ReCounted';", q);
            await context.Database.ExecuteSqlRawAsync(Query);
            return data;
        }

        public async Task<List<Lx17>> CountIvnumLx17(List<Lx17> list)
        {

            CountLx17Req req = new CountLx17Req();
            req.T_LINV.item = mapper.Map<CountLx17Req.T_LINVs.T_LINV_Items[]>(list);
            List<Lx17> data = new List<Lx17>();
            using (client)
            {
                HttpContent contnet =  new StringContent(JsonConvert.SerializeObject(req),Encoding.UTF8,"application/json");
                var res = await client.PostAsync("RESTAdapter/REST/INV_COUNT_EXT", contnet);
                var json = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                    throw new Exception(json);
                var response = JsonConvert.DeserializeObject<CountLx17Res>(json);
                if (response is null)
                    throw new Exception(json);
                for (int i = 0; i < list.Count; i++)
                {
                    data.Add(list[i]);
                    await logService.RegisterCount(data[i]);
                    var lx = await FetchLx17SAP(data[i]);
                    var find = await context.Lx17.Where(x => x.Matnr == lx.Matnr && x.Lgpla == lx.Lgpla && x.Lgtyp == lx.Lgtyp && x.Ivnum == lx.Ivnum && x.Lqnum == lx.Lqnum).FirstOrDefaultAsync();
                    if (find is null)
                        throw new Exception("Recount Material Not Found in Local DB");
                    lx.Id = find.Id;
                    lx.Abcin = find.Abcin;
                    lx.Cntstatus = "Counted";
                    lx.Ymd8up = DateTime.Now;
                    lx.Ymd8in = find.Ymd8in;
                    lx.Istat = "Z";
                    lx.Idusin = find.Idusin;
                    lx.Idusup = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
                    lx.PlantID = find.PlantID;
                    lx.Plant = null;
                    context.Entry(find).CurrentValues.SetValues(lx);
                    await context.SaveChangesAsync();

                }
            }
            return data;
        }

        public async Task<PaginationModel<Lx17>> GetRecountLx17(string plant, Lx17MaterialFilterModel filter)
        {
            var q = context.Lx17.Include(x => x.Plant).Where(x => x.Ivnum != null && x.Plant!.WERKS == plant && x.Istat == "N" && x.Cntstatus == "Recount" &&
            (
                (x.Matnr.Contains(filter.Search ?? "") || x.Ivnum!.Contains(filter.Search ?? "") || filter.Search == "")
                 &&
                (x.Lgtyp.Contains(filter.Lgtyp ?? "") || filter.Lgtyp == "")
            )
            );
            var ttl = await q.CountAsync();
            return new PaginationModel<Lx17>()
            {
                TotalData = ttl,
                firstPage = filter.page == 1,
                lastPage = filter.page == Math.Ceiling((decimal)(ttl / filter.pagesize)),
                PageNum = filter.page,
                Data = await q.Skip((filter.page - 1) * filter.pagesize).Take(filter.pagesize).ToListAsync()
            };
        }

        public async Task<PaginationModel<Lx17>> GetCountableLx17(string plant, Lx17MaterialFilterModel filter)
        {
            var q = context.Lx17.Include(x => x.Plant).Where(x => x.Ivnum != null && x.Plant!.WERKS == plant && x.Istat=="N" &&
            (
                (x.Matnr.Contains(filter.Search ?? "") || x.Ivnum!.Contains(filter.Search ?? "") || filter.Search == "") 
                 &&
                (x.Lgtyp.Contains(filter.Lgtyp ?? "") || filter.Lgtyp == "")
            )
            );
            var ttl = await q.CountAsync();
            return new PaginationModel<Lx17>()
            {
                TotalData = ttl,
                firstPage = filter.page == 1,
                lastPage = filter.page == Math.Ceiling((decimal)(ttl / filter.pagesize)),
                PageNum = filter.page,
                Data = await q.Skip((filter.page - 1) * filter.pagesize).Take(filter.pagesize).ToListAsync()
            };
        }

        public async Task<List<Lx17>> ReCountIvnumLx17(List<Lx17> list)
        {
            CountLx17Req req = new CountLx17Req();
            req.T_LINV.item = mapper.Map<CountLx17Req.T_LINVs.T_LINV_Items[]>(list);
            List<Lx17> data = new List<Lx17>();
            using (client)
            {
                HttpContent contnet = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                var res = await client.PostAsync("RESTAdapter/REST/INV_ReCOUNT_EXT", contnet);
                var json = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                    throw new Exception(json);
                var response = JsonConvert.DeserializeObject<CountLx17Res>(json);
                if (response is null)
                    throw new Exception(json);
                for (int i = 0; i < req.T_LINV.item.Length; i++)
                {
                    data.Add(list[i]);
                    await logService.RegisterRecount(data[i]);

                    var lx = await FetchLx17SAP(data[i]);
                    var find = await context.Lx17.Where(x => x.Matnr == lx.Matnr && x.Lgpla == lx.Lgpla && x.Lgtyp == lx.Lgtyp && x.Ivnum == lx.Ivnum && x.Lqnum == lx.Lqnum).FirstOrDefaultAsync();
                    if (find is null)
                        throw new Exception("Recount Material Not Found in Local DB");
                    lx.Id = find.Id;
                    lx.Abcin = find.Abcin;
                    lx.Cntstatus = "ReCounted";
                    lx.Ymd8up = DateTime.Now;
                    lx.Ymd8in = find.Ymd8in;
                    lx.Idusin = find.Idusin;
                    lx.Idusup = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
                    lx.PlantID = find.PlantID;
                    context.Entry(find).CurrentValues.SetValues(lx);
                    await context.SaveChangesAsync();
                    await logService.RegisterRecount(lx);
                }
            }
            return data;
        }

        public async Task<List<Lx17>> ToggleRecountLx17(List<Lx17> Lx17)
        {
            for (int i=0;i<Lx17.Count;i++)
            {
                var find = await context.Lx17.Where(x => x.Id == Lx17[i].Id).FirstAsync();
                Lx17[i].Istat = "N";
                Lx17[i].Cntstatus = "Recount";
                context.Entry(find).CurrentValues.SetValues(Lx17[i]);
                await context.SaveChangesAsync();
                await logService.ToggleRecount(Lx17[i]);
            }
            return Lx17;
        }
    }
}
