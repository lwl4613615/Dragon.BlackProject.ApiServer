using AutoMapper;
using Dragon.BlackProject.AuthServer.Utils.models;
using Dragon.BlackProject.AuthServer.Utils.Services.JwtService;
using Dragon.BlackProject.BusinessInterface;
using Dragon.BlackProject.BussinessService;
using Dragon.BlackProject.Common;
using Dragon.BlackProject.ModelDtos.User;
using Dragon.BlackProject.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dragon.BlackProject.AuthServer.Controllers
{
    [ApiController]
    [Route("Account/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserManagerService _userManagerService;
        private readonly CustomJWTService _customJWTService;
        private readonly ISqlSugarClient _sqlSugarClient;
        private readonly IMapper _IMapper;
        public LoginController(CustomJWTService jWTService,ISqlSugarClient sqlSugarClient,IMapper mapper, IUserManagerService userManager)
        {
            _customJWTService = jWTService;
            _sqlSugarClient = sqlSugarClient;
            _IMapper = mapper;
            _userManagerService = userManager;
        }
        [HttpPost("Login")]
          public async Task<JsonResult> LoginHandler(User user)
        {
            #region 根据用户的名称& 密码查询到的用户的信息和各种权限
            SysUserInfo? userinfo = _userManagerService.Login(user.Name!, user.Password!);

            if (userinfo == null)
            {
                return await Task.FromResult(new JsonResult(ApiResult<string>.Fail()));
            }
            #endregion
            string accessToken = _customJWTService.GenerateToken(userinfo, out string refreshToken);
            return await Task.FromResult(new JsonResult(ApiResult<string>.Ok(accessToken)));            
        }
    }
}
