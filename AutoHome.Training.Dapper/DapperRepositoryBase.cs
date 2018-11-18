using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AutoHome.Training.Dapper
{
    public abstract class DapperRepositoryBase<TEntity, TPrimaryKey> : IDapperRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        public abstract void Delete([NotNull] TEntity entity);

        public virtual Task DeleteAsync([NotNull] TEntity entity)
        {
            return Task.FromResult(0);
        }

        public abstract int Execute([NotNull] string query, [CanBeNull] object parameters = null);

        public virtual Task<int> ExecuteAsync([NotNull] string query, [CanBeNull] object parameters = null)
        {
            return Task.FromResult(Execute(query, parameters));
        }

        public abstract TEntity FirstOrDefault([NotNull] TPrimaryKey id);

        public virtual Task<TEntity> FirstOrDefaultAsync([NotNull] TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public abstract TEntity Get([NotNull] TPrimaryKey id);

        public abstract IEnumerable<TEntity> GetAll();

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public virtual Task<TEntity> GetAsync([NotNull] TPrimaryKey id)
        {
            return Task.FromResult(Get(id));
        }

        public abstract void Insert([NotNull] TEntity entity);

        public abstract TPrimaryKey InsertAndGetId([NotNull] TEntity entity);

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync([NotNull] TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public virtual Task InsertAsync([NotNull] TEntity entity)
        {
            return Task.FromResult(0);
        }

        public abstract IEnumerable<TEntity> Query([NotNull] string query, [CanBeNull] object parameters = null);

        /// <summary>
        /// AnyData(DTO)
        /// </summary>
        /// <typeparam name="TAny"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract IEnumerable<TAny> Query<TAny>
            ([NotNull] string query, [CanBeNull] object parameters = null) where TAny : class;

        public virtual Task<IEnumerable<TEntity>> QueryAsync([NotNull] string query, [CanBeNull] object parameters = null)
        {
            return Task.FromResult(Query(query, parameters));
        }

        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>([NotNull] string query, [CanBeNull] object parameters = null) where TAny : class
        {
            return Task.FromResult(Query<TAny>(query, parameters));
        }

        public abstract TEntity Single([NotNull] TPrimaryKey id);

        public virtual Task<TEntity> SingleAsync([NotNull] TPrimaryKey id)
        {
            return Task.FromResult(Single(id));
        }

        public abstract void Update([NotNull] TEntity entity);

        public virtual Task UpdateAsync([NotNull] TEntity entity)
        {
            return Task.FromResult(0);
        }
    }
}
