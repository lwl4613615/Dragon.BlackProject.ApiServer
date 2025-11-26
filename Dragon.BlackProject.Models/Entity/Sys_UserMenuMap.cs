using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;
using SqlSugar;
namespace Dragon.BlackProject.Models.Entity
{
    [SugarTable("Sys_UserMenuMap")]
    public class Sys_UserMenuMap
    {
        [SugarColumn(ColumnName ="Id",IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName ="UserId")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "MenuId")]
        public Guid MenuId { get; set; }
    }
}
