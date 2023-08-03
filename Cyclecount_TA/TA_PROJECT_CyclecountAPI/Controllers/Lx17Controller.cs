using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TA_PROJECT_CyclecountAPI.DAL.Services;
using TA_PROJECT_CyclecountAPI.Model.API.Lx17;
using TA_PROJECT_CyclecountAPI.Model.LICC;
using TA_PROJECT_CyclecountAPI.Model.Lx17;
using TA_PROJECT_CyclecountAPI.ViewModel;

namespace TA_PROJECT_CyclecountAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class Lx17Controller : ControllerBase
    {
        private readonly ILx17Service service;
        private readonly ILx17LogService logService;
        private readonly ILICCService liccService;
        private readonly IMapper mapper;
        public Lx17Controller(ILx17Service service, ILICCService liccService,ILx17LogService _service, IMapper mapper)
        {
            this.service = service;
            this.logService = _service;
            this.liccService = liccService;
            this.mapper = mapper;
        }
        [HttpGet("{plant}")]
        public async Task<IActionResult> Index(int page, int pagesize, string plant, string dTo, string dFrom, string? search)
        {
            var data = await service.GetLx17(plant, new Lx17MaterialFilterModel() { page = page, dTo = DateTime.Parse(dTo), dFrom = DateTime.Parse(dFrom), pagesize = pagesize, Search = search });
            return Ok(data);
        }
        [HttpPost("CreateInventoryFromLicc")]
        public async Task<IActionResult> CreateInventoryFromLICC(List<LICC> Data)
        {
            var lx = await service.LICCToLx17(Data);
            lx = await service.CreateInventory(lx);
            return Ok(lx);
        }

        [HttpPost("CreateInventory")]
        public async Task<IActionResult> CreateInventory(List<Lx17> Data)
        {
            var lx = await service.CreateInventory(Data);
            return Ok(lx);
        }

        [HttpGet("FetchSAP")]
        public async Task<IActionResult> FetchSAP(int lgnum, string werks)
        {
            Lx17SAPReq req = new Lx17SAPReq() { IM_LGNUM = lgnum.ToString(), IM_NIGEZ = "x" };
            req.ET_LIST.item.Werks = werks;
            req.ET_LIST.item.Lgnum = lgnum;
            var res = await service.FetchLx17SAP(req);
            return Ok(res);
        }
        [HttpGet("SyncLx17")]
        public async Task<IActionResult> SyncLx17(int lgnum, string werks)
        {

            Lx17SAPReq req = new Lx17SAPReq() { IM_LGNUM = lgnum.ToString(), IM_NIGEZ = "x" };
            req.ET_LIST.item.Werks = werks;
            req.ET_LIST.item.Lgnum = lgnum;
            var res = await service.FetchLx17SAP(req);
            var lx = await service.UpdateLx17(res.ET_LIST.item.ToList());
            return Ok(lx);
        }

        [HttpPost("Count")]
        public async Task<IActionResult> Count(List<Lx17> Data)
        {
            var result = await service.CountIvnumLx17(Data);
            return Ok(result);
        }

        [HttpPost("Recount")]
        public async Task<IActionResult> Recount(List<Lx17> Data)
        {
            var result = await service.ReCountIvnumLx17(Data);
            return Ok(result);
        }
        [HttpPut("Recount")]
        public async Task<IActionResult> RecountUpdate(List<Lx17> Data)
        {
            var ret = await service.ToggleRecountLx17(Data);
            return Ok(ret);
        }
        [HttpGet("Recount")]
        public async Task<IActionResult> GetRecount(string plant, int page, int pagesize, string? search)
        {
            var filter = new Lx17MaterialFilterModel() { page = page, pagesize = pagesize, Search = search ?? "" };
            var res = await service.GetRecountLx17(plant, filter);
            return Ok(res);
        }
        [HttpGet("log/{plantid}")]
        public async Task<IActionResult> GetLog(int plantid, int page, int pagesize, string dFrom, string dTo, string? search)
        {
            var filter = new Lx17MaterialFilterModel() { page = page, pagesize = pagesize,dFrom=DateTime.Parse(dFrom),dTo=DateTime.Parse(dTo), Search = search ?? "" };
            var res = await logService.GetLogLx17(plantid,filter);
            return Ok(res);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> GetCountable(string plant, int page,int pagesize,string? search)
        {
            var filter = new Lx17MaterialFilterModel() { page = page, pagesize = pagesize, Search = search ?? "" };
            var res = await service.GetCountableLx17(plant, filter);
            return Ok(res);

        }
    }
}
