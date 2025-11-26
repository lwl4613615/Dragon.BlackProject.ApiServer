using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace Dragon.BlackProject.Models.Entity
{
    [SugarTable("Sys_RoleBtnMap")]
    public class Sys_RoleBtnMap
    {
        [SugarColumn(ColumnName ="Id",IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }
        [SugarColumn(ColumnName ="RoleId")]
        public int RoleId { get; set; }

        [SugarColumn(ColumnName ="BtnId")]
        public Guid BtnId { get; set; }
    }
}
