using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TA_PROJECT_CyclecountAPI.DAL.Services;
using TA_PROJECT_CyclecountAPI.Model.API.LICC;

namespace TA_PROJECT_CyclecountAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LICCController : ControllerBase
    {   
        private readonly ILICCService lICCService;
        public LICCController(ILICCService lICCService)
        {
            this.lICCService = lICCService;
        }

        [HttpGet("ReadLICC/{count}")]
        public async Task<IActionResult> ReadLicc(int count,int plantid,DateTime dFrom,DateTime dTo,string werks,string lgnum,string classFrom,string classTo,
            string? matnr = null)
        {
            
            return Ok(await lICCService.SyncLicc(new Model.API.LICC.LICCRequest()
            {
                B_DAT = dTo.ToString("yyyy-MM-dd"),//DateTime.Now.AddMonths(5).ToString("yyyy-MM-dd"),
                PDATU = dFrom.ToString("yyyy-MM-dd"),//DateTime.Now.ToString("yyyy-MM-dd"),
                P_WERKS = werks,
                P_COUNT = $"{count.ToString()}",
                P_LGNUM = lgnum,
                S_MATNR = new Model.API.LICC.LICCRequest.Item_S_Matnr()
                {
                    item = (matnr is null || matnr == "") ? new LICCRequest.Item_S_Matnr.Items() : new LICCRequest.Item_S_Matnr.Items()
                    {
                        HIGH = matnr,
                        LOW = matnr,
                        OPTION = "EQ",
                        SIGN = "I"
                    }
                },
                ET_LICC = new LICCRequest.Item_ET_LICC(),
                S_ABCIN = new LICCRequest.Item_S_ABCIN()
                {
                    item = new LICCRequest.Item_S_ABCIN.Items()
                    {
                        HIGH = classTo,
                        LOW = classFrom,
                        OPTION = "EQ",
                        SIGN = "I"
                    }
                },
            },plantid));
        }
        [HttpGet("{plantid}")]
        public async Task<IActionResult> GetLicc(int plantid,int page,int pagesize,string? lgtyp,string? search)
        {
            return Ok(await lICCService.GetLicc(lgtyp?? "",page, pagesize, plantid,search ?? ""));
        }

        [HttpGet("Lgtypes/{plantid}")]
        public async Task<IActionResult> Lgtypes(int plantid)
        {
            return Ok(await lICCService.GetLgTypes(plantid));
        }

    }
}
