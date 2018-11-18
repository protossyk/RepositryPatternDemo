using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.Core
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class
    {
        public abstract Task DeleteAsync(TPrimaryKey Id);

        public abstract Task<IEnumerable<TEntity>> ExecQuerySP(string SPName);

        public async Task<TEntity> Get(TPrimaryKey Id)
        {
            var entity = await FirstOrDefault(Id);
            if (entity == null)
            {
                throw new NotSupportedException();
            }

            return entity;
        }

        public abstract Task<IEnumerable<TEntity>> GetListAsync(string strWhere, object parameters = null);

        public abstract Task<IEnumerable<TEntity>> GetListAsync(object whereConditions);

        public abstract Task<IEnumerable<TEntity>> 
            GetListPage(int pageNumber, int rowsPerPage, string strWhere, string orderBy, object parameters);

        public abstract Task<int?> InsertAsync(TEntity entity);

        public abstract Task<IEnumerable<TEntity>> QueryAsync(string sql, object param = null);

        public abstract Task Update(TEntity entity);
        public virtual Task<TEntity> FirstOrDefault(TPrimaryKey id)
        {
            return Task.FromResult(GetAll().FirstOrDefault());
        }

        //protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        //{
        //    var lambdaParam = Expression.Parameter(typeof(TEntity));

        //    var lambdaBody = Expression.Equal(
        //        Expression.PropertyOrField(lambdaParam, "Id"),
        //        Expression.Constant(id, typeof(TPrimaryKey))
        //        );

        //    return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        //}

        public abstract IEnumerable<TEntity> GetAll();
    }
}
