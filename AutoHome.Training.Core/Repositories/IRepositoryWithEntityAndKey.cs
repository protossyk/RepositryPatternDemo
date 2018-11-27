using AutoHome.Training.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.Core.Repositories
{
    public interface IRepositoryBase<TEntity, TPrimaryKey> : IRepositoryBase where TEntity :class, IEntity<TPrimaryKey>
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        Task<TPrimaryKey> InsertAsync(TEntity entity);
        /// <summary>
        /// 根据ID删除一条数据
        /// </summary>
        Task DeleteAsync(TPrimaryKey Id);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        Task UpdateAsync(TEntity entity);
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// 根据ID获取实体对象
        /// </summary>
        Task<TEntity> Get(TPrimaryKey Id);
      List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="rowsPerPage">每页行数</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="orderBy">Orde by排序</param>
        /// <param name="parameters">parameters参数</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetListPage(int pageNumber, int rowsPerPage, string strWhere, string orderBy, object parameters);
        #endregion
    }
}
