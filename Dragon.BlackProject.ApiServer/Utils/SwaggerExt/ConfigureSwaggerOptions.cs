using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Dragon.BlackProject.ApiServer.Utils.SwaggerExt
{
    //延迟配置SwaggerGen,以便获取到 ApiVersion的信息
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = $"God CedricDadi API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "An ASP.NET Core Web API for Cedric",
                });
            }
            var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
            var xmfPath= Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmfPath))
            {
                options.IncludeXmlComments(xmfPath,includeControllerXmlComments:true);
            }
            
        }
    }
}
