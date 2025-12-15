using Microsoft.AspNetCore.Authorization;

namespace Dragon.BlackProject.ApiServer.Utils.AuthorizationExt
{
    public class MenuAuthorizeHandler : AuthorizationHandler<MenuAuthorizeRequirement>
    {    
        /// <summary>
        /// 解析到TOken以后，通过token中包含的用户信息，去做业务逻辑的判定。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MenuAuthorizeRequirement requirement)
        {
            if (context.User.Claims==null || context.User.Claims.Count()<=0)
            {
                context?.Fail();
            }
            else
            {
                context?.Succeed(requirement);
            }
                
            await Task.CompletedTask;
        }
    }
}
