using Dragon.BlackProject.Common.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // 添加授权服务
            builder.Services.AddAuthorization();

            // 返回 builder 以支持链式调用
            return builder;
        }
    }
}
