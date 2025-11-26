using AutoMapper;
using Dragon.BlackProject.AuthServer.Utils.models;
using Dragon.BlackProject.AuthServer.Utils.Services.JwtService;
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
        private readonly CustomJWTService _customJWTService;
        private readonly ISqlSugarClient _sqlSugarClient;
        private readonly IMapper _IMapper;
        public LoginController(CustomJWTService jWTService,ISqlSugarClient sqlSugarClient,IMapper mapper)
        {
            _customJWTService = jWTService;
            _sqlSugarClient = sqlSugarClient;
            _IMapper = mapper;
        }
        [HttpPost("Login")]
          public async Task<JsonResult> LoginHandler(User user)
        {
            List<Sys_User> userList=await _sqlSugarClient.Queryable<Sys_User>()
                .Where(it=>it.Name==user.Name && it.Password==MD5Encrypt.Encrypt(user.Password))
                .ToListAsync();
            if(userList.Count==0)
            {
                return await Task.FromResult(new JsonResult(ApiResult<string>.Fail(ApiCode.InvalidUserNameOrPassword)));
            }
            Sys_User dbUser=userList[0];
            SysUserInfo userInfo=_IMapper.Map<SysUserInfo>(dbUser);

            List<int> roleIdList=await _sqlSugarClient.Queryable<Sys_UserRoleMap>()
                .Where(it=>it.UserId==dbUser.UserId)
                .Select(it=>it.RoleId)
                .ToListAsync();
            userInfo.RoleIdList=roleIdList;

            List<UserContainsMenus> userMenuMap1 = await _sqlSugarClient.
                Queryable<Sys_RoleMenuMap, Sys_Menu>((map, m) => map.MenuId == m.Id)
                .Where((map, m) => roleIdList.Contains(map.RoleId))
                .Select((map, m) => new UserContainsMenus
                {
                    Id = map.MenuId,
                    Text = m.MenuText
                }).ToListAsync();

            List<UserContainsMenus> userMenuMap2 = await _sqlSugarClient.Queryable<Sys_UserMenuMap, Sys_Menu>((map, m) => map.MenuId == m.Id)
               .Where((map, m) => map.UserId == dbUser.UserId)
               .Select((map, m) => new UserContainsMenus
               {
                   Id = map.MenuId,
                   Text = m.MenuText
               }).ToListAsync();
            List<UserContainsMenus> userBtnMap1 = await _sqlSugarClient.
               Queryable<Sys_RoleBtnMap, Sys_Button>((map, b) => map.BtnId == b.Id)
               .Where((map, b) => roleIdList.Contains(map.RoleId))
               .Select((map, b) => new UserContainsMenus
               {
                   Id = map.BtnId,
                   Text = b.BtnText,
                   Value = b.BtnValue
               }).ToListAsync();

            List<UserContainsMenus> userBtnMap2 = await _sqlSugarClient
                .Queryable<Sys_UserBtnMap, Sys_Button>((map, b) => map.BtnId == b.Id)
                .Where((map, b) => map.UserId == dbUser.UserId)
                .Select((map, b) => new UserContainsMenus
                {
                    Id = map.BtnId,
                    Text = b.BtnText,
                    Value = b.BtnValue
                }).ToListAsync();
            userMenuMap1.AddRange(userMenuMap2);
            userBtnMap1.AddRange(userBtnMap2);
            userInfo.UserMenuList = userMenuMap1;
            userInfo.UserBtnList = userBtnMap1;
            dbUser.LastLoginTime = DateTime.Now;
            _sqlSugarClient.Updateable(dbUser).ExecuteCommand();

            string accessToken= _customJWTService.GenerateToken(userInfo, out string refreshToken);
            return await Task.FromResult(new JsonResult(ApiResult<string>.Ok(accessToken)));            
        }
    }
}
