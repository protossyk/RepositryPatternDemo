using AutoHome.Training.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.Core.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class,IEntity<TPrimaryKey>
    {
        public abstract Task DeleteAsync(TPrimaryKey Id);


        public virtual async Task<TEntity> Get(TPrimaryKey Id)
        {
            var entity = await FirstOrDefault(Id);
            if (entity == null)
            {
                throw new NotSupportedException();
            }

            return entity;
        }


        public abstract Task<IQueryable<TEntity>> 
            GetListPage(int pageNumber, int rowsPerPage, string strWhere, string orderBy, object parameters);

        public abstract Task<TPrimaryKey> InsertAsync(TEntity entity);

        public abstract Task UpdateAsync(TEntity entity);
        public virtual Task<TEntity> FirstOrDefault(TPrimaryKey id)
        {
            return Task.FromResult(GetAll().FirstOrDefault());
        }
        public virtual Task<TEntity> FirstOrDefaultQueryble(TPrimaryKey id)
        {
            return Task.FromResult(GetAll().FirstOrDefault(CreateEqualityExpressionForId(id)));
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public abstract IQueryable<TEntity> GetAll();

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAll().Where(predicate).ToList());
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAll();
        }
    }
}
