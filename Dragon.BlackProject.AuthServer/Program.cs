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
builder.Services.AddAutoMapper(cfg=> { cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzk1NjUxMjAwIiwiaWF0IjoiMTc2NDE2NTY0MyIsImFjY291bnRfaWQiOiIwMTlhYzA3NDFhZTA3YmU0YjU4ZTIxNzg0ZmFkZDc1ZCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa2IwN2V2bm16bXFxMmZxZWZ0aGV6YzJxIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.TpBOqoUjP3MKy2deTgdbFVqZAeiagXvgEHPJI8r1dtRkJo92dN85gVqpBceHLdh_bBG83nfe7nBARXJzej1H0szZli-riR7J6oeJe9k9ElTnIi5SHaqwFrsF2zjTZu44V5jAT2PFGjvyjVfzjRoYrXT5NScI03VmCFtwo0ixm0Pjn71_Jl5fjsQm-X-T3xUukany2ZQoInv59q6bgKeUv3n1TymCWx4ckZx1Og2WSQYTqbbTreOjvL2NeD-ic23wJEnTym_UF0BJqpE51NCR8Cn7wpct6T2zkUmdh-Zdc4hiWRzd8nJNfCkBB3d8shyoVesjq_u9Npd83zY6MilvAw"; },typeof(AutoMapperConfigs));
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
