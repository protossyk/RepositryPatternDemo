using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Mongo
{
    public class MongoContextOptions : IOptions<MongoContextOptions>
    {
        public MongoContextOptions Value => this.Value;
        public string ConnectionString { get; set; }

        public string DatatabaseName { get; set; }
    }
}
