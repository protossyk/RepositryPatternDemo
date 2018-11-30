using AutoHome.Training.Core;
using AutoHome.Training.Core.Entities;
using AutoHome.Training.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Mongo
{
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, int>, IRepositoryBase<TEntity>
        where TEntity : class, IEntity<int>
    {
        public MongoDbRepositoryBase(IMongoContext mongoContext)
            : base(mongoContext)
        {
        }
    }
}
