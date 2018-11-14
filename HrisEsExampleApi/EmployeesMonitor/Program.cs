using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using EmployeesMonitor.AppInit;
using EmployeesMonitor.BuildTable;
using EmployeesMonitor.MaintainTable;

namespace EmployeesMonitor
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            Console.WriteLine("Please press <ESC> to exit.");

            var builder = new ContainerBuilder();

            builder.RegisterModule<MediatrModule>();
            builder.RegisterModule<AppModule>();

            var container = builder.Build();

            var loader = container.Resolve<IPastEventLoader>();
            var listener = container.Resolve<IEventListener>();

            var tasks = new[]
            {
                new Task(loader.Run), 
                new Task(listener.Start)
            };

            foreach (var task in tasks)
                task.Start();

            Task.WaitAll(tasks);

            Console.ReadKey();
        }

    }
}
