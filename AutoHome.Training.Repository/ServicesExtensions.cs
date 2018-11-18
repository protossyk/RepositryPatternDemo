using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoHome.Training.Dapper;
using System.Data.SqlClient;

namespace AutoHome.Training.Repository
{
    public static class ServicesExtensions
    {
        public static IServiceCollection UseSQLDapper(this IServiceCollection services,string connectionStr)
        {
            return services.UseDapperDbProvider<TraingRepositoryContext>(
                option => {
                    option.DbProviderFactory = SqlClientFactory.Instance;
                    option.ConnectionString = connectionStr;
                });
        }
    }
}
