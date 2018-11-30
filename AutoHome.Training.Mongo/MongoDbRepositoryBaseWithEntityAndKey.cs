using AutoHome.Training.Core;
using AutoHome.Training.Core.Entities;
using AutoHome.Training.Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.Mongo
{
    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
           where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IMongoContext _mongoContext;
        public MongoDbRepositoryBase(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public virtual MongoCollection<TEntity> Collection
        {
            get
            {
                return _mongoContext.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        public override Task<TEntity> Get(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            var entity = Collection.FindOne(query);
            if (entity == null)
            {
                throw new NotSupportedException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return Task.FromResult(entity);
        }
        public override Task DeleteAsync(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            Collection.Remove(query);
            return Task.CompletedTask;
        }

        public override Task<IQueryable<TEntity>> GetListPage(int pageNumber, int rowsPerPage, string strWhere, string orderBy, object parameters)
        {
            return Task.FromResult(Collection.FindAll().Skip((pageNumber - 1) * rowsPerPage).Take(rowsPerPage)
                .AsQueryable());
        }

        public override Task<TPrimaryKey> InsertAsync(TEntity entity)
        {
            Collection.Insert(entity);
            return Task.FromResult(entity.Id);
        }

        public override Task UpdateAsync(TEntity entity)
        {
            Collection.Save(entity);
            return Task.FromResult(entity);
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }
    }
}
