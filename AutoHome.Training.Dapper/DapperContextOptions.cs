using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AutoHome.Training.Dapper
{
    public class DapperContextOptions : IOptions<DapperContextOptions>
    {
        public DapperContextOptions Value => this;
        public DbProviderFactory DbProviderFactory {get;set;}
        public string ConnectionString { get; set; } = string.Empty;
    }
}
