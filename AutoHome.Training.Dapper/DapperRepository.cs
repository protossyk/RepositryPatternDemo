using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AutoHome.Training.Dapper
{
    public class DapperRepository<TContext,TEntity, TPrimaryKey> : DapperRepositoryBase<TEntity, TPrimaryKey>
            where TEntity : class
        where TContext: RepositoryContextBase
    {
        private readonly TContext _context;
        public DapperRepository(TContext context)
        {
            _context = context;
        }


        public virtual DbConnection Connection
        {
            get { return (DbConnection)_context.Conn; }
        }
        public virtual IDbTransaction DbTransaction
        {
            get { return _context.Tran; }
        }


        public override TEntity Single(TPrimaryKey id)
        {
            return Connection.Get<TEntity>(id, DbTransaction);
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            var whereStr = $" WHERE Id=@Id";
            return Connection.GetList<TEntity>(whereStr, new
            {
                Id = id
            }, DbTransaction)?.FirstOrDefault();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            if (item == null) { throw new NotSupportedException(); }//change customized exception

            return item;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            return Connection.GetList<TEntity>(DbTransaction);
        }
        public override Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Connection.GetListAsync<TEntity>(DbTransaction);
        }

        public override IEnumerable<TEntity> Query
            (string query, object parameters = null)
        {
            return Connection.Query<TEntity>(query, parameters, transaction:DbTransaction);
        }

        public override Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return Connection.QueryAsync<TEntity>(query, parameters, DbTransaction);
        }

        public override IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            return Connection.Query<TAny>(query, parameters, DbTransaction);
        }

        public override Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            return Connection.QueryAsync<TAny>(query, parameters, DbTransaction);
        }

        public override int Execute(string query, object parameters = null)
        {
            return Connection.Execute(query, parameters, DbTransaction);
        }

        public override Task<int> ExecuteAsync(string query, object parameters = null)
        {
            return Connection.ExecuteAsync(query, parameters, DbTransaction);
        }


        public override void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public override void Update(TEntity entity)
        {
            Connection.Update(entity, DbTransaction);
        }

        public override void Delete(TEntity entity)
        {
            Connection.Delete(entity, DbTransaction);
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            TPrimaryKey primaryKey = Connection.Insert<TPrimaryKey, TEntity>(entity);

            return primaryKey;
        }

        public override Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return  Connection.InsertAsync<TPrimaryKey, TEntity>(entity, DbTransaction);
        }
    }
}
