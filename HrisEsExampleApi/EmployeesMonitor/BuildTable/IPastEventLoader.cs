using EventStore.ClientAPI;

namespace EmployeesMonitor.BuildTable
{
    public interface IPastEventLoader
    {
        CatchUpSubscriptionSettings Settings { get; }

        void Run();
    }
}