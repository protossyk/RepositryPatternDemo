using AutoHome.Training.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace AutoHome.Training.Dapper
{
    public class RepositoryContextBase : DisposableObject, IRepositoryContextBase
    {
        private readonly DbProviderFactory _dbProviderFactory;
        private DbConnection _conn;
        private readonly string _connectionStr;
        public RepositoryContextBase(
            DbProviderFactory dbProviderFactory,
            string connectionStr
            )
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionStr = connectionStr;
        }
        public IDbConnection Conn
        {
            get
            {
                try
                {
                    if (_conn == null)
                    {
                        _conn = _dbProviderFactory.CreateConnection();
                        _conn.ConnectionString = _connectionStr;
                        _conn.Open();
                    }
                }
                catch(Exception ex)
                {
                    //log
                    throw new Exception("数据库连接出错", ex);
                }

                return _conn;
            }
        }
        public IDbTransaction Tran { private set; get; }
        private readonly object _sync = new object();
        public bool Committed { set; get; } = true;
        private bool disposed = false;
        public void InitConnection()
        {
            _conn.ConnectionString = _connectionStr;
            //_conn.Open();

        }
        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                if (this._conn != null && this._conn.State != ConnectionState.Closed)
                {
                    this._conn.Close();
                    this._conn.Dispose();
                }

            }
            disposed = true;
        }

        public void BeginTran()
        {
            this.Tran = this.Conn.BeginTransaction();
            this.Committed = false;
        }

        public void Commit()
        {
            if (Committed) return;
            lock (_sync)
            {
                this.Tran.Commit();
                this.Committed = true;
            }
        }

        //public void Rollback()
        //{
        //    if (Committed) return;
        //    lock (_sync)
        //    {
        //        this.Tran.Rollback();
        //        this.Committed = true;
        //    }
        //}
    }
}
