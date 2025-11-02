using Asp.Versioning.ApiExplorer;

namespace Dragon.BlackProject.ApiServer.Utils.SwaggerExt
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// 使用版本化的Swagger中间件。
        /// </summary>
        public static WebApplication UseVersionedSwagger(this WebApplication app)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
            return app;
        }
    }
}
