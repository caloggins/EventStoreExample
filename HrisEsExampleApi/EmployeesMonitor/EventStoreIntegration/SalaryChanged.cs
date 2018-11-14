namespace EmployeesMonitor.EventStoreIntegration
{
    public class SalaryChanged : Event
    {
        public decimal Salary;
        public string Reason;
    }
}