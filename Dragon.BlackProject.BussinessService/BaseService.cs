using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Dragon.BlackProject.BusinessInterface;

namespace Dragon.BlackProject.BusinessInterface
{
    public abstract class BaseService : IBaseService
    {

        protected readonly ISqlSugarClient _Client = null;

        public BaseService(ISqlSugarClient sqlSugarClient)
        {
            _Client = sqlSugarClient;
        }

        #region Query---查询相关的

        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : class
        {
            return _Client.Queryable<T>().InSingle(id);
        }
        /// <summary>
        /// 主键查询-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> FindAsync<T>(int id) where T : class
        {
            return await _Client.Queryable<T>().InSingleAsync(id);
        }
        /// <summary>
        /// 不应该暴露给上端使用者，尽量少用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("尽量避免,using 带表达式目录树的代替")]
        public ISugarQueryable<T> Set<T>() where T : class
        {
            return _Client.Queryable<T>();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funWhere"></param>
        /// <returns></returns>

        public ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funWhere) where T : class
        {
            return _Client.Queryable<T>().Where(funWhere);
        }


        public PagingData<T> QueryPage<T>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, object>> funcOrderBy, bool isAsc = true) where T : class
        {
            var list = _Client.Queryable<T>();
            if (funcWhere != null)
            {
                list = list.Where(funcWhere);
            }
            list = list.OrderByIF(true, funcOrderBy, isAsc ? OrderByType.Asc : OrderByType.Desc);
            PagingData<T> pagingData = new PagingData<T>()
            {
                DataList = list.ToPageList(pageIndex, pageSize),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = list.Count()
            };
            return pagingData;
        }
        #endregion

        #region Insert---新增相关的
        /// <summary>
        /// 新增数据-同步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Insert<T>(T t) where T : class, new()
        {
            return _Client.Insertable(t).ExecuteReturnEntity();

        }
        /// <summary>
        /// 新增数据 -异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>

        public async Task<T> InsertAsync<T>(T t) where T : class, new()
        {
            return await _Client.Insertable(t).ExecuteReturnEntityAsync();
        }

        public async Task<bool> InsertList<T>(List<T> tList) where T : class, new()
        {
            return await _Client.Insertable(tList).ExecuteCommandIdentityIntoEntityAsync();
        }

        #endregion

        #region Update--更新相关
        /// <summary>
        /// 是没有实现查询，直接更新的,需要Attach和State
        /// 
        /// 如果是已经在context，只能再封装一个(在具体的service)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public async Task<bool> UpdateAsync<T>(T t) where T : class, new()
        {
            if (t == null) throw new Exception("t is null");

            return await _Client.Updateable(t).ExecuteCommandHasChangeAsync();
        }

        public void Update<T>(List<T> tList) where T : class, new()
        {
            _Client.Updateable(tList).ExecuteCommand();
        }

        #endregion


        #region Delete--删除相关
        /// <summary>
        /// 先附加，再删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Delete<T>(T t) where T : class, new()
        {
            _Client.Deleteable(t).ExecuteCommand();
        }

        public bool Delete<T>(object pId)where T : class, new()
        {
            T t = _Client.Queryable<T>().InSingle(pId);
            return _Client.Deleteable<T>(t).ExecuteCommand() > 0;
        }

        public void Delete<T>(List<T> tList) where T:class
        {
            _Client.Deleteable(tList).ExecuteCommand();
        }
        #endregion

        #region Other -执行Sql语句的
        ISugarQueryable<T> IBaseService.ExcuteQuery<T>(string sql) where T : class
        {
            return _Client.SqlQueryable<T>(sql);
        }
        public void Dispose()
        {
            if (_Client != null)
            {
                _Client.Dispose();
            }
        }
        #endregion

    }
}
