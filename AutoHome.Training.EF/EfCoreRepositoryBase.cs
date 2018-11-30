using AutoHome.Training.Core;
using AutoHome.Training.Core.Entities;
using AutoHome.Training.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.EF
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, int>, IRepositoryBase<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(TDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
