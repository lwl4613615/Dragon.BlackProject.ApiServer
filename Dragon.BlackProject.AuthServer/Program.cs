using Dragon.BlackProject.AuthServer.Utils.InitDatabaseExt;
using Dragon.BlackProject.AuthServer.Utils.Services.JwtService;
using Dragon.BlackProject.BusinessInterface.Map;
using Dragon.BlackProject.Common.Jwt;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ÃÌº”JwtOptions≈‰÷√
builder.Services.Configure<JwtTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
builder.Services.AddSingleton<CustomJWTService, CustomHSJWTService>();
builder.Services.AddAutoMapper(cfg=> { },typeof(AutoMapperConfigs));
builder.InitSqlSugar();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
