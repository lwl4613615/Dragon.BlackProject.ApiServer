using Dm.Config;
using Dragon.BlackProject.Common.EnumEnity;
using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Dragon.BlackProject.Models.Entity
{
    [SugarTable("Sys_User")]
    public class Sys_User:Sys_BaseModel
    {
        [SugarColumn(ColumnName = "UserId", IsIdentity=true, IsPrimaryKey=true)]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

        [SugarColumn(IsNullable = true)]
        public int UserType { get; set; } = (int)UserTypeEnum.GeneralUser;

        [SugarColumn(IsNullable=true)]
        public string? Phone { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? Mobile { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? Address { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? Email { set; get; }
        public string? QQ { set; get; }

        [SugarColumn(IsNullable = true)]
        public string? WeChat { set; get; }

        public int Sex { set; get; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string? Imageurl { set; get; }

        public DateTime LastLoginTime { set; get; }

    }
}
