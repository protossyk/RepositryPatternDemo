﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
