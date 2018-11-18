using AutoHome.Training.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.EF
{
    public class EfCoreUnitOfWork<TContext> : UnitOfWorkBase
        where TContext:DbContext
    {
        private readonly DbContext _context;
        public EfCoreUnitOfWork(TContext context)
        {
            _context = context;
            //EF Core 默认开启事务操作
        }
        public override void SaveChanges()
        {
            //EF CORE 可以AOP+Dic 方式获取和一次性保存所有DbContext
            _context?.SaveChanges();
        }

        protected override void DisposeUow()
        {
            _context.Dispose();
        }
    }
}
