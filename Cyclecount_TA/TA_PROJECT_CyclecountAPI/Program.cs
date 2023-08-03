using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using TA_PROJECT_CyclecountAPI.Config;
using TA_PROJECT_CyclecountAPI.DAL;
using TA_PROJECT_CyclecountAPI.DAL.Services;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var authConfig = config.GetSection("Auth").Get<AuthConfig>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:44351", "http://localhost:65408", "http://localhost:4200", "http://10.155.152.114:80", "http://10.155.152.114", "http://localhost", "https://eajdigitization.se.com/cyclecount/")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(x =>
  {
      x.RequireHttpsMetadata = false;
      x.SaveToken = true;
      x.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authConfig.Key)),
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          RequireExpirationTime = true,
          ValidIssuer = authConfig.Issuer,
          ValidAudience = authConfig.Audience,
      };
  });

builder.Services.AddHttpClient("SAP_API", cfg =>
{
    cfg.BaseAddress = new Uri(config["API_URL"]);
    cfg.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
        Convert.ToBase64String(Encoding.UTF8.GetBytes("PO_CYCLECOUNT:Bridge@1")//Username Password Should be Masked in future
    ));
    cfg.Timeout = cfg.Timeout * 10;
}).ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
{

    ServerCertificateCustomValidationCallback = (w, x, y, z) => true
});
builder.Services.AddDbContext<CyclecountContext>(
    option => {
        option.UseSqlServer(config.GetConnectionString("QAS"),opt=>opt.EnableRetryOnFailure());
        option.EnableSensitiveDataLogging(true);
        });
builder.Services.AddSingleton(authConfig);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(AutoMapConfig));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILICCService, LICCService>();
builder.Services.AddScoped<ILx17LogService, Lx17LogService>();
builder.Services.AddScoped<ILx17Service, Lx17Service>();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();   

app.Run();
