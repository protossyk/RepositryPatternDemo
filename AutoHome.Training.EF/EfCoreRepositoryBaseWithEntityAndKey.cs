using AutoHome.Training.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.EF
{
    public class EfCoreRepositoryBase<TDbContext, TEntity, TPrimaryKey> :
        RepositoryBase<TEntity, TPrimaryKey>,
        IRepositoryWithDbContext

        where TEntity : class
        where TDbContext : DbContext
    {
        public override Task DeleteAsync(TPrimaryKey Id)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> ExecQuerySP(string SPName)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public DbContext GetDbContext()
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> GetListAsync(string strWhere, object parameters = null)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> GetListAsync(object whereConditions)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> GetListPage(int pageNumber, int rowsPerPage, string strWhere, string orderBy, object parameters)
        {
            throw new NotImplementedException();
        }

        public override Task<int?> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> QueryAsync(string sql, object param = null)
        {
            throw new NotImplementedException();
        }

        public override Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
