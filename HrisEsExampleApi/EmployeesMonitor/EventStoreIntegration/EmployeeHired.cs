using System;

namespace EmployeesMonitor.EventStoreIntegration
{
    public class EmployeeHired : Event
    {
        public string Name;
        public decimal Salary;
        public Guid EmployeeId;
    }
}