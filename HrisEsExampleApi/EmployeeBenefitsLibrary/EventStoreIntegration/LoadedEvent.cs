namespace EmployeeBenefitsLibrary.EventStoreIntegration
{
    public class LoadedEvent : EventFromStream
    {
        public byte[] Data;
        public string Type;
    }
}