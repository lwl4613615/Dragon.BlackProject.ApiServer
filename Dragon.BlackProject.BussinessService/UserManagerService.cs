using AutoMapper;
using Dragon.BlackProject.BusinessInterface;
using Dragon.BlackProject.Common;
using Dragon.BlackProject.ModelDtos.User;
using Dragon.BlackProject.Models.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon.BlackProject.BussinessService
{
    public class UserManagerService:BaseService,IUserManagerService
    {
        private IMapper _IMapper;
        public UserManagerService(ISqlSugarClient sqlSugarClient,IMapper mapper):base(sqlSugarClient)
        {
            _IMapper = mapper;
        }

        /// <summary>
        /// 登录功能
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUserInfo? Login(string userName, string password)
        {
            string pwd = MD5Encrypt.Encrypt(password);
            List<Sys_User> userList = _Client
            .Queryable<Sys_User>()
            .Where(c => c.Name.Equals(userName) && c.Password.Equals(pwd))
            .ToList();
            if (userList == null || userList.Count <= 0)
            {
                return null;
            }
            Sys_User user = userList.First();
            SysUserInfo userinfo = _IMapper.Map<Sys_User, SysUserInfo>(user);

            List<int> roleIdList = _Client
                .Queryable<Sys_UserRoleMap>()
                .Where(c => c.UserId == user.UserId)
                .Select(r => r.RoleId).ToList();

            userinfo.RoleIdList = roleIdList;

            List<UserContainsMenus> userMenuMap1 = _Client
                .Queryable<Sys_RoleMenuMap, Sys_Menu>((map, m) => map.MenuId == m.Id)
                  .Where((map, m) => roleIdList.Contains(map.RoleId))
                  .Select((map, m) => new UserContainsMenus
                  {
                      Id = map.MenuId,
                      Text = m.MenuText
                  }).ToList();

            List<UserContainsMenus> userMenuMap2 = _Client
                  .Queryable<Sys_UserMenuMap, Sys_Menu>((map, m) => map.MenuId == m.Id)
                  .Where((map, m) => map.UserId == user.UserId)
                   .Select((map, m) => new UserContainsMenus
                   {
                       Id = map.MenuId,
                       Text = m.MenuText
                   }).ToList();


            List<UserContainsMenus> userBtnMap1 = _Client
                .Queryable<Sys_RoleBtnMap, Sys_Button>((map, b) => map.BtnId == b.Id)
                  .Where((map, b) => roleIdList.Contains(map.RoleId))
                  .Select((map, b) => new UserContainsMenus
                  {
                      Id = map.BtnId,
                      Text = b.BtnText,
                      Value = b.BtnValue
                  }).ToList();

            List<UserContainsMenus> userBtnMap2 = _Client
                  .Queryable<Sys_UserBtnMap, Sys_Button>((map, b) => map.BtnId == b.Id)
                  .Where((map, b) => map.UserId == user.UserId)
                   .Select((map, b) => new UserContainsMenus
                   {
                       Id = map.BtnId,
                       Text = b.BtnText,
                       Value = b.BtnValue
                   }).ToList();


            userMenuMap1.AddRange(userMenuMap2);
            userBtnMap1.AddRange(userBtnMap2);

            userinfo.UserMenuList = userMenuMap1;
            userinfo.UserBtnList = userBtnMap1;

            user.LastLoginTime = DateTime.Now;
            _Client.Updateable(user).ExecuteCommand();

            return userinfo;
        }
    }
}
