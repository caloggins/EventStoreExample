using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;

namespace EmployeesMonitor.Database
{
    public class AddEmployee : IRequest<Unit>
    {
        public Guid EmployeeId;
        public string EmployeeName;
        public decimal Salary;
    }

    [UsedImplicitly]
    public class AddEmployeeHandler : IRequestHandler<AddEmployee, Unit>
    {
        private readonly IHrisDatabase database;

        public AddEmployeeHandler(IHrisDatabase database)
        {
            this.database = database;
        }

        public async Task<Unit> Handle(AddEmployee request, CancellationToken token)
        {
            var found = await database.Employees
                .FirstOrDefaultAsync(o => o.EmployeeId == request.EmployeeId, token);

            if (found != null)
                return Unit.Value;

            var employee = new Employee
            {
                EmployeeId = request.EmployeeId,
                Salary = request.Salary,
                EmployeeName = request.EmployeeName
            };
            database.Employees.Add(employee);

            await database.SaveChangesAsync(token);

            return Unit.Value;
        }
    }
}