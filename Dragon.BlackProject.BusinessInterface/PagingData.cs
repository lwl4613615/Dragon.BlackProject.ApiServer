using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon.BlackProject.BusinessInterface
{
    public class PagingData<T> where T : class
    {
        /// <summary>
        /// 符号条件查询的数据总条数
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 查询的第几页
        /// </summary>
        public int  PageIndex { get; set; }

        /// <summary>
        /// 第一页的数据数量
        /// </summary>
        public int  PageSize { get; set; }

        /// <summary>
        /// 当前页的数据源
        /// </summary>
        public List<T>? DataList { get; set; }
    }
}
