using AutoHome.Training.Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AutoHome.Training.Repository
{
    public class TraingRepositoryContext: RepositoryContextBase
    {
        public TraingRepositoryContext(
            DbProviderFactory dbProviderFactory,
            string connectionStr
            ):base(dbProviderFactory,connectionStr)
        {
        }
    }
}
