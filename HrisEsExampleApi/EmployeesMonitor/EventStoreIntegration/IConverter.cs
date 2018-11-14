namespace EmployeesMonitor.EventStoreIntegration
{
    public interface IConverter
    {
        Event ConvertReadToDomain(string type, byte[] data);
    }
}