using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TA_PROJECT_CyclecountAPI.Model.API.LICC;
using TA_PROJECT_CyclecountAPI.Model.LICC;
using TA_PROJECT_CyclecountAPI.ViewModel;

namespace TA_PROJECT_CyclecountAPI.DAL.Services
{
    public interface ILICCService
    {
        Task<List<LICC>?> SyncLicc(LICCRequest request,int plantid);
        Task<PaginationModel<LICC>> GetLicc(string lgtyp,int page, int pagesize, int plant,string search);
        Task RemoveLICC(string matnr, string lgpla, string lgtyp, string lqnum);
        Task<string[]> GetLgTypes(int plantid);
    }
    public class LICCService : ILICCService
    {
        private readonly CyclecountContext context;
        private readonly HttpClient client;
        private readonly ClaimsPrincipal user;
        private readonly IAuthService authService;
        public LICCService(CyclecountContext context,IHttpClientFactory factory,IHttpContextAccessor httpContext,IAuthService auth)
        {
            this.context = context;
            user = httpContext.HttpContext!.User;
            authService = auth;
            client = factory.CreateClient("SAP_API");
        }
        
        public async Task<List<LICC>?> SyncLicc(LICCRequest request,int plantId)
        {
            LICCResponse? data = new LICCResponse();
            using (client)
            {
                var parsed = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(parsed,Encoding.UTF8,"application/json");
                var res = await client.PostAsync("RESTAdapter/REST/LICC_READ", content);
                var s = await res.Content.ReadAsStringAsync();
                if (res.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<LICCResponse>(s);
                    var list = data!.ET_LICC.item;
                    var _user = (await authService.GetUsers(user.FindFirstValue(ClaimTypes.NameIdentifier))).First();
                    for (int i=0;i<list.Length;i++)
                    {
                        list[i].Idusin = _user.Id;
                        list[i].Idusup = _user.Id;
                        list[i].PlantId= plantId;
                        list[i].Plant = null;
                        list[i].UpdatedBy = null;
                        list[i].InsertedBy = null;
                    }
                    context.LICC.RemoveRange(await context.LICC.Where(x => x.PlantId == plantId && x.Idusin == _user.Id).ToArrayAsync());
                    await context.SaveChangesAsync();
                    await context.LICC.AddRangeAsync(list);
                    await context.SaveChangesAsync();
                }
            }
            return data?.ET_LICC.item.ToList() ?? null;
        }

        public async Task<PaginationModel<LICC>> GetLicc(string lgtyp, int page,int pagesize, int plant,string search)
        {
            page = page - 1;
            var _user = (await authService.GetUsers(user.FindFirstValue(ClaimTypes.NameIdentifier))).First();
            var data =  context.LICC.Where(x => x.Idusin == _user.Id && x.PlantId == plant && (lgtyp == "" || x.LGTYP==lgtyp) &&
            (
                (x.MATNR.Contains(search)) || (x.LGPLA.Contains(search)) || x.LQNUM.Contains(search)  || search == ""
            )
            );
            decimal ttl = await data.CountAsync();
            return new PaginationModel<LICC>()
            {
                Data = await data.Skip(page * pagesize).Take(pagesize).ToListAsync(),
                TotalData = (int)Math.Ceiling(ttl),
                firstPage = page + 1 == 1,
                lastPage = page + 2 == Math.Ceiling(ttl / pagesize),
                PageNum = page+1
            };
        }
        public async Task<string[]> GetLgTypes(int plantid)
        {
            int id = int.Parse(user.FindFirstValue("Id"));
            var data = await context.LICC.Where(x => x.Idusin == id && x.PlantId == plantid).Select(x => x.LGTYP).Distinct().ToArrayAsync();
            return data;
        }
        public async Task RemoveLICC(string matnr,string lgpla,string lgtyp,string lqnum)
        {
            var find = await context.LICC.Where(x => x.MATNR == matnr && x.LGPLA == lgpla && x.LGTYP == lgtyp && x.LQNUM == lqnum).FirstOrDefaultAsync();
            if (find is null)
                throw new Exception("Error LICC NOT FOUND");
            context.Remove(find);
            await context.SaveChangesAsync();
        }
    }
}
