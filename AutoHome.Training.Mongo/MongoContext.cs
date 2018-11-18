using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AutoHome.Training.Mongo
{
    public class MongoContext : IMongoContext
    {
        MongoContextOptions _options;
        public MongoContext(IOptions<MongoContextOptions> options)
        {
            _options = options.Value;
        }
        public MongoDatabase Database =>  new MongoClient(_options.ConnectionString)
                .GetServer()
                .GetDatabase(_options.DatatabaseName);
    }
}
