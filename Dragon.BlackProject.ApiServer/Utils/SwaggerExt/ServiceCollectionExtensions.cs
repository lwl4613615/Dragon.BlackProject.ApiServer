using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.CompilerServices;

namespace Dragon.BlackProject.ApiServer.Utils.SwaggerExt
{
    /// <summary>
    /// 高内聚的文档入口，供WEB API项目复用。
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册API版本控制、版本探索器和SwaggerGen.
        /// 外部可传入回调追加自定义的Swagger配置。
        /// </summary>
        
        public static IServiceCollection AddVersionedSwagger(
            this IServiceCollection services,
            Action<SwaggerGenOptions>? configureSwagger = null
            )
        {
            //AddVersioning直接返回ApiversioningBuilder,可继续链式配置。
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            if (configureSwagger != null)
            {
                services.PostConfigure(configureSwagger);
            }

            return services;

        }

    }
}
