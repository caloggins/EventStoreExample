using System;
using StackExchange.Redis;

namespace EmployeesMonitor
{
    public class RedisAdapter
    {
        public void Start()
        {
            var multiplexer = ConnectionMultiplexer.Connect("localhost");
            var database = multiplexer.GetDatabase();

            database.StringSet("sample", 12);

            var value = (int)database.StringGet("sample");

            Console.WriteLine($"Value is {value}.");

        }
    }
}