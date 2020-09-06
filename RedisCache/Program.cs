using System;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace RedisCache
{
    class Program
    {
        private static readonly string connectionString = "<redis_connection_string>";

        static async Task Main(string[] args)
        {
            using(var cache = ConnectionMultiplexer.Connect(connectionString))
            {
                IDatabase db = cache.GetDatabase();
                var setValue = await db.StringSetAsync("test:key", "100");
                Console.WriteLine($"SET: {setValue}");

                var getValue = await db.StringGetAsync("test:key");
                Console.WriteLine($"GET: {getValue}");

                var result = await db.ExecuteAsync("ping");
                Console.WriteLine($"PING = {result.Type} : {result}");

                result = await db.ExecuteAsync("flushdb");
                Console.WriteLine($"FLUSHDB = {result.Type} : {result}");
            }
        }
    }
}
