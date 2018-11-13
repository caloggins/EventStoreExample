using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EmployeesMonitor
{
    class Program
    {
        static  void Main()
        {
            try
            {
                Console.WriteLine("-- Starting console. --");

                var connection = EventStoreConnection.Create("ConnectTo=tcp://HrisDemo:N&mD8Le5OWv0@qa-sandbox-114:1113");

                connection.ConnectAsync().Wait();

                var subscriber = new Subscriber();

                var settings = new CatchUpSubscriptionSettings(
                    int.MaxValue,
                    1,
                    false,
                    true);
                connection.SubscribeToStreamFrom(
                    "Employee",
                    0L,
                    settings,
                    (subscription, evt) => subscriber.ProcessEvent(evt));

                connection.SubscribeToStreamAsync(
                    "Employee",
                    true,
                    (subscription, evt) => subscriber.ProcessEvent(evt))
                    .Wait();

                Console.WriteLine("Subscription created. Press a key at any time to close the connection.");
                Console.ReadKey();

                connection.Close();
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

    public class Subscriber
    {
        public void ProcessEvent(ResolvedEvent evt)
        {
            var type = evt.Event.EventType;
            Console.WriteLine($"  Received event of type: {type}");
        }
    }
}
