using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TA_PROJECT_CyclecountAPI.Config;
using TA_PROJECT_CyclecountAPI.Model.User;
using TA_PROJECT_CyclecountAPI.ViewModel;

namespace TA_PROJECT_CyclecountAPI.DAL.Services
{
    public interface IAuthService
    {
        Task<string?> Login(LoginModel model);
        Task<UserModel> Register(UserModel model);
        Task Delete(string sesaid);
        Task<UserModel> UpdateModel(UserModel model, string sesaid);
        Task<List<UserModel>> GetUsers();
        Task<List<UserModel>> GetUsers(string search);
        Task<PaginationModel<UserModel>> GetUsers(int page,int pagesize,string plant);
        Task<PaginationModel<UserModel>> GetUsers(int page, int pagesize,string plant,string? searc);
        Task<bool> isUserExists(string SesaId);
        Task RegisterWithRolePlant(UserModel user,RoleModel role,DeptModel plant);
    }
    public class AuthService : IAuthService
    {
        private readonly AuthConfig config;
        private readonly CyclecountContext context;
        public AuthService(CyclecountContext _context,AuthConfig _cfg)
        {
            context = _context;
            config = _cfg;
        }
        public async Task Delete(string sesaid)
        {
            var find = await context.Users.Where(x => x.SESAID==sesaid).FirstOrDefaultAsync();
            if (find is null)
                throw new Exception("User Can't Be Found");
            context.Users.Remove(find);
            await context.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }
        public async Task<List<UserModel>> GetUsers(string search)
        {
            return await  context.Users.Include(x => x.DefaultDept).Where(x =>
            (
                (x.SESAID == search) ||
                (x.Username == search) ||
                (x.Name == search) ||
                search == ""
            )).ToListAsync();
        }

        public async Task<PaginationModel<UserModel>> GetUsers(int page, int pagesize, string plant)
        {

            page = page - 1;
            var find = context.Users.Include(x => x.DefaultDept).Where(x => x.DefaultDept.DeptName == plant).Skip(pagesize * page).Take(page);
            var Ttl = await find.CountAsync();
            return new PaginationModel<UserModel>()
            {
                TotalData = Ttl,
                Data = await find.ToListAsync(),
                PageNum = page+1,
                firstPage = page == 0,
                lastPage = (page+2) == Math.Ceiling(((decimal)Ttl)/((decimal)pagesize))
            };
        }

        public async Task<PaginationModel<UserModel>> GetUsers(int page, int pagesize,string plant, string? search)
        {
            page = page - 1;
            var find = context.Users.Include(x => x.DefaultDept).Where(x => x.DefaultDept.DeptName == plant && 
            (
                (x.SESAID==search) ||
                (x.Username==search) ||
                (x.Name==search) ||
                search == ""
            )).Skip(pagesize * page).Take(page);
            var Ttl = await find.CountAsync();
            return new PaginationModel<UserModel>()
            {
                TotalData = Ttl,
                Data = await find.ToListAsync(),
                PageNum = page + 1,
                firstPage = page == 0,
                lastPage = (page + 2) == Math.Ceiling(((decimal)Ttl) / ((decimal)pagesize))
            };
        }
        private string GenerateToken(UserModel model)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,model.SESAID),
                new Claim("Dept",model.DefaultDept.DeptName),
                new Claim("SesaId",model.SESAID),
                new Claim("Username",model.Username),
                new Claim("Id",model.Id.ToString()),
                new Claim("Name",model.Name),
                new Claim("Level",model.Level),
                new Claim("Userid",model.SESAID),
                new Claim("Depts",JsonSerializer.Serialize(model.Depts.Select(x=>x.Dept))),
                new Claim("Role",JsonSerializer.Serialize(model.Role)),
                new Claim(ClaimTypes.Sid,model.Id.ToString()),
                new Claim(ClaimTypes.Role,model.Role!.RoleName)
            };
            var token = new JwtSecurityToken(
                issuer: config.Issuer,
                audience: config.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credential);
            return tokenhandler.WriteToken(token);
        }
        public string Hash(string Text)
        {
            StringBuilder sb = new StringBuilder();
            using (HMACSHA512 sha = new HMACSHA512())
            {
                sha.Key = Encoding.UTF8.GetBytes(config.Key);
                byte[] raw = Encoding.UTF8.GetBytes(Text);
                byte[] res = sha.ComputeHash(raw);
                foreach (var data in res)
                {
                    sb.Append(data.ToString("x2"));
                }
            }
            return sb.ToString();
        }

        public async Task<bool> isUserExists(string SesaId)
        {
            return await context.Users.AnyAsync(x => x.SESAID == SesaId);
        }

        public async Task<string?> Login(LoginModel model)
        {
            model.Password = Hash(model.Password);
            var find = await context.Users.Where(x => x.Username == model.Username && x.Password == model.Password).Include(x=>x.Role).Include(x=>x.DefaultDept).Include(x=>x.Depts).ThenInclude(x=>x.Dept).FirstOrDefaultAsync();
            if (find is not null)
                return GenerateToken(find);
            return null;
        }

        public async Task<UserModel> Register(UserModel model)
        {
            if (await isUserExists(model.SESAID))
                throw new Exception("User already exists");
            model.Password = Hash(model.Password);
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return await context.Users.Where(x=>x.SESAID==model.SESAID).FirstAsync();
        }
        public async Task RegisterWithRolePlant(UserModel user,RoleModel role,DeptModel plant)
        {

            if (await isUserExists(user.SESAID))
                throw new Exception("User already exists");
            if (await context.Roles.Where(x=>x.RoleName==role.RoleName).AnyAsync())
                throw new Exception("Role already exists");
            if (await context.Depts.Where(x=>x.DeptName==plant.DeptName).AnyAsync())
                throw new Exception("Dept already exists");
            /* await context.Depts.AddAsync(plant);
             await context.SaveChangesAsync();
             await context.Roles.AddAsync(role);
             await context.SaveChangesAsync();
             await context.Entry(role).ReloadAsync();
             await context.Entry(plant).ReloadAsync();
             user.RoleId = role.Id;
             user.DefaultDeptId = plant.Id;
             user.Role = null;
             await context.AddAsync(user);
             await context.SaveChangesAsync();
             await context.AddAsync(new UserDeptModel()
             {
                 DeptId = plant.Id,
                 UserId = user.Id
             });
             await context.SaveChangesAsync();*/
            user.Role = role;
            user.DefaultDept = plant;
            user.Password = Hash(user.Password);
            user.Depts = new List<UserDeptModel>() { new UserDeptModel() { Dept = plant, User = user } };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<UserModel> UpdateModel(UserModel model, string sesaid)
        {
            if (await isUserExists(model.SESAID))
                throw new Exception("User already exists");
            var find = await context.Users.Where(x => x.SESAID == sesaid).FirstAsync();
            model.Id = find.Id;
            context.Entry(find).CurrentValues.SetValues(model);
            return model;
        }
    }
}
