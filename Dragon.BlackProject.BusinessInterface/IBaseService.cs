using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dragon.BlackProject.BusinessInterface
{
    public interface IBaseService
    {
        #region Query-查询相关
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <return></return>
        T Find<T>(int id)where T : class;

        /// <summary>
        /// 主键查询-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync<T>(int id) where T : class;

        /// <summary>
        /// 提供对单表的查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("尽量避免使用，Using 带表达式目录树的代替")]
        ISugarQueryable<T> Set<T>() where T : class;

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funWhere"></param>
        /// <returns></returns>
        ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funWhere) where T : class;

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="funcOrderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        PagingData<T> QueryPage<T>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, object>>funcOrderBy, bool isAsc = true) where T : class;

        #endregion

        #region Add-新增相关
        /// <summary>
        /// 新增数据-同步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T Insert<T>(T t) where T : class, new();
        /// <summary>
        /// 新增数据-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<T> InsertAsync<T>(T t) where T: class, new();

        Task<bool> InsertList<T>(List<T> tList) where T : class, new();

        #endregion

        #region Update-更新
        /// <summary>
        /// 更新数据 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(T t)where T:class,new();

        /// <summary>
        /// 更新数据 ，即时commit.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        void Update<T>(List<T> tList) where T : class, new();

        #endregion

        #region Delete-删除相关
        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pId"></param>
        /// <returns></returns>
        bool Delete<T>(object pId) where T : class, new();

        /// <summary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void Delete<T>(T t) where T : class, new();

        /// <summary>
        /// 删除数据，即时commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tlist"></param>
        void Delete<T>(List<T> tlist) where T : class;

        #endregion

        #region Other-执行Sql
        /// <summary>
        /// 执行sql 返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        ISugarQueryable<T> ExcuteQuery<T>(string sql) where T : class, new();
        #endregion
    }
}
