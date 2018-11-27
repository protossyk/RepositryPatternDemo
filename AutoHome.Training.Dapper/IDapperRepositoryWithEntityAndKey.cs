using AutoHome.Training.Core.Repositories;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AutoHome.Training.Dapper
{
    public interface IDapperRepository<TEntity, TPrimaryKey> : IRepositoryBase where TEntity : class
    {
        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [NotNull]
        TEntity Single([NotNull] TPrimaryKey id);



        /// <summary>
        ///     Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [NotNull]
        Task<TEntity> SingleAsync([NotNull] TPrimaryKey id);

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [NotNull]
        TEntity Get([NotNull] TPrimaryKey id);

        /// <summary>
        ///     Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [NotNull]
        Task<TEntity> GetAsync([NotNull] TPrimaryKey id);

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CanBeNull]
        TEntity FirstOrDefault([NotNull] TPrimaryKey id);

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CanBeNull]
        Task<TEntity> FirstOrDefaultAsync([NotNull] TPrimaryKey id);




        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAll();

        /// <summary>
        ///     Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllAsync();




        /// <summary>
        ///     Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> Query([NotNull] string query, [CanBeNull] object parameters = null);

        /// <summary>
        ///     Queries the asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> QueryAsync([NotNull] string query, [CanBeNull] object parameters = null);

        /// <summary>
        ///     Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TAny> Query<TAny>([NotNull] string query, [CanBeNull] object parameters = null) where TAny : class;

        /// <summary>
        ///     Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TAny>> QueryAsync<TAny>([NotNull] string query, [CanBeNull] object parameters = null) where TAny : class;

        /// <summary>
        ///     Executes the given query text
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int Execute([NotNull] string query, [CanBeNull] object parameters = null);

        /// <summary>
        ///     Executes as async the given query text
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        Task<int> ExecuteAsync([NotNull] string query, [CanBeNull] object parameters = null);


        /// <summary>
        ///     Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert([NotNull] TEntity entity);

        /// <summary>
        ///     Inserts the and get identifier.
        /// </summary>
        /// <param name="entity">The entity.</param>
        TPrimaryKey InsertAndGetId([NotNull] TEntity entity);

        /// <summary>
        ///     Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [NotNull]
        Task InsertAsync([NotNull] TEntity entity);

        /// <summary>
        ///     Inserts the and get identifier asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [NotNull]
        Task<TPrimaryKey> InsertAndGetIdAsync([NotNull] TEntity entity);

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update([NotNull] TEntity entity);

        /// <summary>
        ///     Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        [NotNull]
        Task UpdateAsync([NotNull] TEntity entity);

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete([NotNull] TEntity entity);


        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [NotNull]
        Task DeleteAsync([NotNull] TEntity entity);

    }
}
