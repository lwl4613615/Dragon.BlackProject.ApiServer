using Dragon.BlackProject.ApiServer.Utils.Filter;
using Dragon.BlackProject.ApiServer.Utils.SwaggerExt;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(a => a.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 14, // 保留 14 天，可按需调整
        shared: true,
        encoding: Encoding.UTF8))
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog();
builder.Services.AddControllers(options=>
{
    options.Filters.Add<CustomAsyncExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddVersionedSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseVersionedSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
