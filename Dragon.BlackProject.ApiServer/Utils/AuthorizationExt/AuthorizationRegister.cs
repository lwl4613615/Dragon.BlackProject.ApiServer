using Dragon.BlackProject.Common;
using Dragon.BlackProject.Common.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dragon.BlackProject.ApiServer.Utils.AuthorizationExt
{
    public static class AuthorizationRegister
    {
        public static WebApplicationBuilder AuthorizationExt(this WebApplicationBuilder builder)
        {
            // 方式1：从配置直接获取（推荐）
            var jwtSection = builder.Configuration.GetSection("JwtTokenOptions");
            var jwtTokenOptions = jwtSection.Get<JwtTokenOptions>();

            // 验证配置
            if (jwtTokenOptions == null)
            {
                throw new InvalidOperationException("JwtTokenOptions configuration is missing");
            }

            if (string.IsNullOrEmpty(jwtTokenOptions.SecretKey))
            {
                throw new InvalidOperationException("JWT SecretKey is not configured");
            }

            // 注册配置到DI容器
            builder.Services.Configure<JwtTokenOptions>(jwtSection);

            // 添加认证服务
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = jwtTokenOptions.Audience,
                    ValidIssuer = jwtTokenOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtTokenOptions.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };

                // 添加事件处理
                options.Events = new JwtBearerEvents
                {

                    /// <summary>
                    ///  处理认证失败的情况，例如令牌无效或过期
                    ///  </summary>    
                    OnChallenge = async context => {
          
                        context.HandleResponse();
                        var payload = JsonSerializer.Serialize(ApiResult<string>.Fail(ApiCode.Unauthorized,401),new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            Encoder=System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        });  
                        context.Response.ContentType = "application/json;charset=utf-8";
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        await context.Response.WriteAsync(payload);     

                    },
                    /// <summary>
                    /// 处理没有权限访问资源的情况
                    /// </summary> 
                    OnForbidden = async context =>
                    {                        
                        var payload = JsonSerializer.Serialize(ApiResult<string>.Fail(ApiCode.Forbidden, "对不起，你没有权限访问该URL."), new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        });
                        context.Response.ContentType = "application/json;charset=utf-8";
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        await context.Response.WriteAsync(payload);

                    }                   

                };
            });

            // 添加授权服务
            builder.Services.AddAuthorization(options=>
            {
                options.AddPolicy("btnPolicy", policyBuilder => policyBuilder
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .AddRequirements(new MenuAuthorizeRequirement()));
            });
            builder.Services.AddTransient<IAuthorizationHandler, MenuAuthorizeHandler>();
            // 返回 builder 以支持链式调用
            return builder;
        }
    }
}
