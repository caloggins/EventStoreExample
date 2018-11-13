using System;

namespace EmployeesMonitor
{
    static class Program
    {
        static  void Main()
        {
            EventListener listener = null;

            try
            {
                Console.WriteLine("-- Starting console. --");
                Console.WriteLine("App is running. Pressing any key at any time will exit.");

                listener = new EventListener();
                listener.Start();

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

            listener?.Dispose();
        }

    }
}
