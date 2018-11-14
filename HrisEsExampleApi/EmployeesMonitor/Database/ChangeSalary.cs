using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EmployeesMonitor.Database
{
    public class ChangeSalary : IRequest<Unit>
    {
        public Guid EmployeeId;
        public decimal Salary;
    }

    public class ChangeSalaryHandler : IRequestHandler<ChangeSalary, Unit>
    {
        private readonly IHrisDatabase database;

        public ChangeSalaryHandler(IHrisDatabase database)
        {
            this.database = database;
        }

        public async Task<Unit> Handle(ChangeSalary request, CancellationToken token)
        {
            var employee = await database.Employees
                .FirstOrDefaultAsync(o => o.EmployeeId == request.EmployeeId, token);

            if(employee == null)
                return Unit.Value;

            employee.Salary = request.Salary;

            await database.SaveChangesAsync(token);

            return Unit.Value;
        }
    }
}