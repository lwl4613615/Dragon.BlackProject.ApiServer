using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace Dragon.BlackProject.Models.Entity
{
    /// <summary>
    /// 角色 
    /// </summary>
    /// 
    [SugarTable("Sys_Role")]
    public class Sys_Role:Sys_BaseModel
    {
        [SugarColumn(ColumnName ="RoleId",IsPrimaryKey = true,IsIdentity =true)]
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        /// 
        [SugarColumn(ColumnName = "RoleName")]
        public string? RoleName { get; set; }

    }
}
