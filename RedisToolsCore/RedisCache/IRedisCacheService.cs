using System.Collections.Generic;

namespace RedisToolsCoreCore
{
    public interface IRedisCacheService
    {
        T GetObject<T>(string key,int dbNumber=0);
        bool SetObject<T>(string key, T value, int dbNumber=0);
        List<T> GetList<T>(string key, int dbNumber = 0);
        bool SetList<T>(string key, List<T> value, int dbNumber = 0);
        bool InsertToEndOfList<T>(string key, T value, int dbNumber = 0);

        bool RemoveKey(string key, int dbNumber = 0);
    }
}