using System;
using System.Linq;

namespace EmployeesMonitor.EventStoreIntegration
{
    public static class StreamExtensions
    {
        public static Guid GetIdFromStream(this string stream)
        {
            var guid = stream.Split('.')
                .Last();

            return Guid.Parse(guid);
        }
    }
}