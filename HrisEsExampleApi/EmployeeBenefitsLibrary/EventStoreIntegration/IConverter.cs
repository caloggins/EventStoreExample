namespace EmployeeBenefitsLibrary.EventStoreIntegration
{
    public interface IConverter
    {
        Event ConvertReadToDomain(TypeMap types, LoadedEvent eventFromStream);
    }
}