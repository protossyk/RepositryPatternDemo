using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Core
{
    /// <summary>
    /// base implement for all uow classes
    /// </summary>
    public abstract class UnitOfWorkBase : DisposableObject,IUnitOfWork
    {
        private bool disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                DisposeUow();
            }
            //handler and envent
            disposed = true;
        }

        public abstract void SaveChanges();
        protected abstract void DisposeUow();
    }
}
