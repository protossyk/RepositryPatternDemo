using AutoHome.Training.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Dapper
{
    /// <summary>
    /// Dapper UOW :
    /// 1.Ensure Conection Opened.
    /// 2.Trsaction Begin. 
    /// 3.UpdateDB.
    /// 4.Commit Or RollBack.
    /// 5.Ensure Repository And UOW using the same connection and trasaction(Context)
    /// </summary>
    public class DapperUnitOfWork : UnitOfWorkBase
    {
        private readonly RepositoryContextBase _repositoryContextBase;
        public DapperUnitOfWork(RepositoryContextBase repositoryContextBase)
        {
            _repositoryContextBase = repositoryContextBase;
            _repositoryContextBase.BeginTran();
        }
        public override void SaveChanges()
        {
            if (_repositoryContextBase.Committed)
            {
                throw new InvalidOperationException("Transaction has already been commit or dispose unexpectedly. ");
            }
            _repositoryContextBase.Commit();
        }

        /// <summary>
        /// Dispose UOW
        /// </summary>
        protected override void DisposeUow()
        {
            //When ITransaction Dispose,RollBack() happens.
            //if (!_repositoryContextBase.Committed)
            //{
            //    _repositoryContextBase.Rollback();
            //}
            _repositoryContextBase.Dispose();
        }
    }
}
