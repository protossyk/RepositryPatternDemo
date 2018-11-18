using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AutoHome.Training.Dapper
{
    public static class DapperExtensions
    {
        public static IServiceCollection UseDapperDbProvider<TContext>(
            this IServiceCollection services,
            Action<DapperContextOptions> dbBuildAction
            )
            where TContext: class, IRepositoryContextBase
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (dbBuildAction == null)
            {
                throw new ArgumentNullException(nameof(dbBuildAction));
            }
            services.Configure(dbBuildAction);
            services.AddScoped<IRepositoryContextBase, TContext>();
            return services;
        }
    }
}
