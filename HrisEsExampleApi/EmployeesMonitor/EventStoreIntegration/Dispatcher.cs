using System;
using System.Threading.Tasks;
using EmployeesMonitor.Database;
using JetBrains.Annotations;
using MediatR;

namespace EmployeesMonitor.EventStoreIntegration
{
    [UsedImplicitly]
    public class Dispatcher : IDispatcher
    {
        private readonly IMediator mediator;

        public Dispatcher(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Dispatch(EmployeeHired evt, Guid employeeId)
        {
            var command = new AddEmployee
            {
                Salary = evt.Salary,
                EmployeeId = employeeId,
                EmployeeName = evt.Name
            };
            await mediator.Send(command);
        }

        public async Task Dispatch(Terminated evt, Guid employeeId)
        {
            var command = new DeleteEmployee
            {
                EmployeeId = employeeId
            };
            await mediator.Send(command);
        }

        public async Task Dispatch(SalaryChanged evt, Guid employeeId)
        {
            var command = new ChangeSalary
            {
                Salary = evt.Salary,
                EmployeeId = employeeId
            };
            await mediator.Send(command);
        }
    }
}