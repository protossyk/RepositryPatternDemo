using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.Core
{
    public interface IRepositoryBase<TEntity, TPrimaryKey> : IRepositoryBase where TEntity : class
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        Task<int?> InsertAsync(TEntity entity);
        /// <summary>
        /// 根据ID删除一条数据
        /// </summary>
        Task DeleteAsync(TPrimaryKey Id);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        Task Update(TEntity entity);
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// 根据ID获取实体对象
        /// </summary>
        Task<TEntity> Get(TPrimaryKey Id);
        Task<IEnumerable<TEntity>> QueryAsync(string sql, object param = null);
        /// <summary>
        /// 根据条件获取实体对象集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListAsync(string strWhere, object parameters = null);

        /// <summary>
        /// 根据查询对象获取列表
        /// </summary>
        /// <param name="whereConditions">查询对象</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListAsync(object whereConditions);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="rowsPerPage">每页行数</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="orderBy">Orde by排序</param>
        /// <param name="parameters">parameters参数</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListPage(int pageNumber, int rowsPerPage, string strWhere, string orderBy, object parameters);
        /// <summary>
        /// 无参存储过程
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ExecQuerySP(string SPName);
        #endregion
    }
}
