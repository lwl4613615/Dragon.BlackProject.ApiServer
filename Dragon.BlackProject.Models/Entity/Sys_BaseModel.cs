using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace Dragon.BlackProject.Models.Entity
{
    public abstract class Sys_BaseModel
    {
        public DateTime CreateTime { get; set; }=DateTime.Now;

        [SugarColumn(IsNullable=true)]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 状态 0:正常 1:冻结 2:删除
        /// </summary>

        public int Status { get; set; }
    }
}
