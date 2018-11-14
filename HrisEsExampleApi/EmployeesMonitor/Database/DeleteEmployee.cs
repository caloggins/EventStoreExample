using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EmployeesMonitor.Database
{
    public class DeleteEmployee : IRequest<Unit>
    {
        public Guid EmployeeId;
    }

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee, Unit>
    {
        private readonly IHrisDatabase database;

        public DeleteEmployeeHandler(IHrisDatabase database)
        {
            this.database = database;
        }

        public async Task<Unit> Handle(DeleteEmployee request, CancellationToken token)
        {
            var employee = await database.Employees
                .FirstOrDefaultAsync(o => o.EmployeeId == request.EmployeeId, token);

            if(employee == null)
                return Unit.Value;

            database.Employees.Remove(employee);

            await database.SaveChangesAsync(token);

            return Unit.Value;
        }
    }
}