using Dragon.BlackProject.ModelDtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon.BlackProject.BusinessInterface
{
    public interface IUserManagerService : IBaseService
    {
        /// <summary>
        /// 登录成功查询到的用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUserInfo? Login(string userName, string password);
    }
}
