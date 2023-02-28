using Newtonsoft.Json;
using RedisToolsCoreCore;

namespace RedisToolsCore
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IRedisStore _redisStore;
        public RedisCacheService(IRedisStore redisStore)
        {
            _redisStore = redisStore;
        }

        public T GetObject<T>(string key, int dbNumber = 0)
        {
            var value = _redisStore.RedisDatabase(dbNumber).StringGet(key);
            if (!value.IsNull)
                return JsonConvert.DeserializeObject<T>(value);

            return default(T);
        }
        public bool SetObject<T>(string key, T value, int dbNumber = 0)
        {
            try
            {
                _redisStore.RedisDatabase( dbNumber ).StringSet(key, JsonConvert.SerializeObject(value));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get all elements of specified list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string key, int dbNumber = 0)
        {
            var result = _redisStore.RedisDatabase(dbNumber).ListRange(key, 0, _redisStore.RedisDatabase(dbNumber).ListLength(key));

            var list = new List<T>();
            foreach (var item in result)
                list.Add(JsonConvert.DeserializeObject<T>(item));

            return list ?? null;
        }

        public bool SetList<T>(string key, List<T> value, int dbNumber = 0)
        {
            try
            {
                foreach (var item in value)
                    _redisStore.RedisDatabase(dbNumber).ListRightPush(key, JsonConvert.SerializeObject(item));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Insert to end of list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool InsertToEndOfList<T>(string key, T value, int dbNumber = 0)
        {
            try
            {
                _redisStore.RedisDatabase( dbNumber ).ListRightPush(key, JsonConvert.SerializeObject(value));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveKey(string key, int dbNumber = 0)
        {
            return _redisStore.RedisDatabase(dbNumber).KeyDelete(key);
        }
    }
}