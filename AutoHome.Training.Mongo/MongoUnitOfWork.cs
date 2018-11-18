using AutoHome.Training.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Mongo
{
    /// <summary>
    /// Mongo doesn't support ACID cross the documents
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class MongoUnitOfWork<TContext> : UnitOfWorkBase
        where TContext : MongoContext
    {
        public override void SaveChanges()
        {
           
        }

        protected override void DisposeUow()
        {
            
        }
    }
}
