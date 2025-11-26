using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace Dragon.BlackProject.Models.Entity
{
    [SugarTable("Sys_UserBtnMap")]
    public class Sys_UserBtnMap
    {
        [SugarColumn(ColumnName ="Id",IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }
        [SugarColumn(ColumnName ="UserId")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName ="BtnId")]
        public Guid BtnId { get; set; }
    }
}
