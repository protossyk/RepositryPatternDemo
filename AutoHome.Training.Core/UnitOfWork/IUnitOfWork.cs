using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
