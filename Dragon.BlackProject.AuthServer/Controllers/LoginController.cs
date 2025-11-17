using Dragon.BlackProject.AuthServer.Utils.models;
using Dragon.BlackProject.AuthServer.Utils.Services.JwtService;
using Dragon.BlackProject.Common;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dragon.BlackProject.AuthServer.Controllers
{
    [ApiController]
    [Route("Account/[controller]")]
    public class LoginController : Controller
    {
        private readonly CustomJWTService _customJWTService;
        public LoginController(CustomJWTService jWTService)
        {
            _customJWTService = jWTService;
        }
        [HttpPost("Login")]
          public async Task<JsonResult> LoginHandler(User user)
        {
            string accessToken= _customJWTService.GenerateToken(user,out string refreshToken);
            return await Task.FromResult(new JsonResult(ApiResult<string>.Ok(accessToken)));            
        }
    }
}
