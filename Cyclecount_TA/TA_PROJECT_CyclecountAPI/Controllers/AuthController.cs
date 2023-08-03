using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TA_PROJECT_CyclecountAPI.DAL.Services;
using TA_PROJECT_CyclecountAPI.Model.User;
using TA_PROJECT_CyclecountAPI.ViewModel;

namespace TA_PROJECT_CyclecountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;
        public AuthController(IAuthService service)
        {
            this.service = service;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var s = await service.Login(login);
            if (s is not null)
            {
                Response.Headers.Authorization = $"Bearer {s}"; 
            }
            return s is null ? NotFound() : Ok(new { response = s });
        }
        [HttpPost("Register/{rolename}")]
        public async Task<IActionResult> Register(UserModel login,string rolename,string plant)
        {
            await service.RegisterWithRolePlant(login, new RoleModel() { RoleName=rolename,Level=5},new DeptModel()
            {
                DeptName = plant,
                LGNUM = "221",
                WERKS="2020",
                Mltp = 1
            });
            return Ok(await service.GetUsers());
        }

        [Authorize]
        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await service.GetUsers());
        }
    }
}
