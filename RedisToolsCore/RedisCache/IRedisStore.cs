﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisToolsCore
{
    public interface IRedisStore
    {
        IDatabase RedisDatabase(int dbNumber);
    }
}
