using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace Dragon.BlackProject.Models.Entity
{
    [SugarTable("Sys_RoleMenuMap")]

    public class Sys_RoleMenuMap:Sys_BaseModel
    {
        [SugarColumn(ColumnName ="Id",IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName ="RoleId")]
        public int RoleId { get; set; }

        [SugarColumn(ColumnName ="MenuId")]
        public Guid MenuId { get; set; }


    }
}
