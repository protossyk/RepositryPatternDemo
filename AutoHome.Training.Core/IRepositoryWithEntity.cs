using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Core
{
    public interface IRepositoryBase<TEntity> : IRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
    }
}
