using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisToolsCore
{
    public class RedisStore : IRedisStore
    {
        private readonly Lazy<ConnectionMultiplexer> LazyConnection;  
        public RedisStore()
        {
            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
        }
        public ConnectionMultiplexer Connection => LazyConnection.Value;
        public IDatabase RedisDatabase(int dbNumber) => Connection.GetDatabase(dbNumber);


    }
}