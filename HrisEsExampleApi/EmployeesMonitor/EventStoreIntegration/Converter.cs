using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace EmployeesMonitor.EventStoreIntegration
{
    [UsedImplicitly]
    public class Converter : IConverter
    {
        private readonly List<(string name, Type type)> types = new List<(string name, Type type)>
        {
            (typeof(EmployeeHired).Name, typeof(EmployeeHired)),
            (typeof(SalaryChanged).Name, typeof(SalaryChanged)),
            (typeof(Terminated).Name, typeof(Terminated))
        };

        public Event ConvertReadToDomain(string type, byte[] data)
        {
            var nameOfType = types.Single(o => o.name == type).type;
            var json = Encoding.UTF8.GetString(data);
            var evt = JsonConvert.DeserializeObject(json, nameOfType)
                as Event;
            return evt;
        }
    }
}