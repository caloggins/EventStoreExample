using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using StackExchange.Redis;

namespace EmployeesMonitor
{
    class Program
    {
        static  void Main()
        {
            try
            {
                Console.WriteLine("-- Starting console. --");
                Console.WriteLine("App is running. Pressing any key at any time will exit.");

                var multiplexer = ConnectionMultiplexer.Connect("localhost");
                var database = multiplexer.GetDatabase();

                database.StringSet("sample", 12);

                var value = (int)database.StringGet("sample");

                Console.WriteLine($"Value is {value}.");

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("-- Press any key to exit. --");
                Console.ReadKey();
            }
        }

    }
}
