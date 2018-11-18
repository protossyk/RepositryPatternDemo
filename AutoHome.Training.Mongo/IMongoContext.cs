using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Mongo
{
    public interface IMongoContext
    {
        MongoDatabase Database { get; }
    }
}
