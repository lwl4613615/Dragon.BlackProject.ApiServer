using Dragon.BlackProject.Common.EnumEnity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon.BlackProject.ModelDtos.User
{
    public class SysUserInfo
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public int UserType { get; set; }=(int)UserTypeEnum.GeneralUser;

        public int Status { get; set; }

        public string? Phone { get; set; }

        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public  long QQ { get; set; }

        public string? WeChat { get; set; }

        public int Sex { get; set; }

        public string? Imageurl { get; set; }

        public List<int>? RoleIdList { get; set; } = new List<int>();

        public List<UserContainsMenus>? UserMenuList { get; set; }=new List<UserContainsMenus>();

        public List<UserContainsMenus>? UserBtnList { get; set; }=new List<UserContainsMenus>();
    }
}
