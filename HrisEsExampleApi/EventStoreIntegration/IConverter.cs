using EmployeeBenefitsLibrary;

namespace EventStoreIntegration
{
    public interface IConverter
    {
        Event ConvertReadToDomain(TypeMap types, LoadedEvent eventFromStream);
    }
}