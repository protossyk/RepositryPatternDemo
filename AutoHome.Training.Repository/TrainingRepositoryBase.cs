using AutoHome.Training.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Repository
{
    public class TrainingRepositoryBase<TEntity, TPrimaryKey> 
        : DapperRepository<RepositoryContextBase, TEntity, TPrimaryKey>
        where TEntity:class
    {
        public TrainingRepositoryBase(
            RepositoryContextBase repositoryContextBase
            ):base(repositoryContextBase)
        {

        }
    }
}
