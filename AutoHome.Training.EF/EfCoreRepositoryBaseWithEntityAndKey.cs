using AutoHome.Training.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.EF
{
    public class EfCoreRepositoryBase<TDbContext, TEntity, TPrimaryKey> :
        RepositoryBase<TEntity, TPrimaryKey>,
        IRepositoryWithDbContext

        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        public EfCoreRepositoryBase(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        //public virtual DbTransaction Transaction
        //{
        //    get
        //    {
        //        return (DbTransaction)_dbContext?.Database.CurrentTransaction?.GetDbTransaction();
        //    }
        //}

        public virtual DbConnection Connection
        {
            get
            {
                var connection = Context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;
            }
        }
        public async override Task DeleteAsync(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                AttachIfNot(entity);
                Table.Remove(entity);
                return ;
            }

            entity = await FirstOrDefaultQueryble(id);
            if (entity != null)
            {
                AttachIfNot(entity);
                Table.Remove(entity);
                return;
            }
        }

        public override IQueryable<TEntity> GetAll()
        {
            return GetAllIncluding();
        }

        public DbContext GetDbContext() => _dbContext;
        /// <summary>
        /// for subclass override getting the Context
        /// </summary>
        public virtual TDbContext Context => _dbContext;

        public override Task<IQueryable<TEntity>> GetListPage(int pageNumber, int rowsPerPage, string strWhere, string orderBy, object parameters)
        {
            return Task.FromResult(Table.Skip((pageNumber - 1) * rowsPerPage).Take(rowsPerPage));
        }

        public override Task<TPrimaryKey> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Table.Add(entity).Entity.Id);
        }


        public override Task UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }
        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }
        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (propertySelectors != null && propertySelectors.Any())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }
    }
}
