using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.EF
{
    /// <summary>
    /// EF has DBContext,so it is easy to have a repository and uow model.
    /// </summary>
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
