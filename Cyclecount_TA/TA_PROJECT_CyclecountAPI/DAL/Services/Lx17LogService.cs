using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TA_PROJECT_CyclecountAPI.Model.Lx17;
using TA_PROJECT_CyclecountAPI.ViewModel;

namespace TA_PROJECT_CyclecountAPI.DAL.Services
{
    public interface ILx17LogService
    {
        Task<Lx17Log> RegisterCount(Lx17 data);
        Task<Lx17Log> RegisterInventory(Lx17 data);
        Task<Lx17Log> ToggleRecount(Lx17 data);
        Task<Lx17Log> RegisterRecount(Lx17 data);

        Task<PaginationModel<Lx17Log>> GetLogLx17(int plantid,Lx17MaterialFilterModel filter);
    }
    public class Lx17LogService : ILx17LogService
    {
        private readonly CyclecountContext context;
        private readonly ClaimsPrincipal user;
        private readonly IMapper mapper;
        public Lx17LogService(CyclecountContext context,IMapper mapper,IHttpContextAccessor acc)
        {
            this.context = context;
            this.mapper = mapper;
            user = acc.HttpContext!.User;
        }

        public async Task<Lx17Log> RegisterInventory(Lx17 data)
        {
            return await InsertLog(data, 1);
        }
        public async Task<Lx17Log> RegisterCount(Lx17 data)
        {
            return await InsertLog(data, 2);
        }
        public async Task<Lx17Log> ToggleRecount(Lx17 data)
        {
            return await InsertLog(data, -1);
        }

        public async Task<Lx17Log> RegisterRecount(Lx17 data)
        {
            return await InsertLog(data, 3);

        }
        private async Task<Lx17Log> InsertLog(Lx17 data,int action)
        { 

            Lx17Log Log = mapper.Map<Lx17Log>(data);
            Log.Id = null;
            Log.Cntstatus = data.Cntstatus;
            Log.Action = action;
            Log.Idusup = int.Parse(user.FindFirstValue(ClaimTypes.Sid));
            Log.Logged_UserId = int.Parse(user.FindFirstValue(ClaimTypes.Sid));
            Log.Ymd8up = DateTime.Now;
            Log.Ymd8Log = DateTime.Now;
            Log.Lx17Id = data.Id;
                Log.Plant = null;
            await context.AddAsync(Log);
            await context.SaveChangesAsync();
            await context.Entry(Log).ReloadAsync();
            return Log;
        }

        public async Task<PaginationModel<Lx17Log>> GetLogLx17(int plantid,Lx17MaterialFilterModel filter)
        {
            var Query = context.Lx17Log.Where(x =>
                x.Ymd8Log >= filter.dFrom && x.Ymd8Log <= filter.dTo &&
                x.Matnr.Contains(filter.Search ?? "") && x.Ivnum!.Contains(filter.Search??"") && x.PlantID==plantid
            );
            var ttl = await Query.CountAsync();
            return new PaginationModel<Lx17Log>()
            {
                Data = await Query.Skip(filter.pagesize * (filter.page-1)).Take(filter.pagesize ).OrderByDescending(x => x.Ymd8Log).OrderByDescending(x=>x.Ymd8Log).ToListAsync(),
                firstPage = filter.page <= 1,
                lastPage = filter.page == Math.Ceiling((double)ttl / filter.pagesize),
                PageNum = filter.page,
                TotalData = ttl
            };
        }
    }
}
