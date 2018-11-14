using System;
using System.Threading.Tasks;

namespace EmployeesMonitor.EventStoreIntegration
{
    public interface IDispatcher
    {
        Task Dispatch(EmployeeHired evt, Guid employeeId);
        Task Dispatch(Terminated evt, Guid employeeId);
        Task Dispatch(SalaryChanged evt, Guid employeeId);
    }
}