using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDataLibrary
{
    public class GetSalaryForEmployee : IRequest<decimal>
    {
        public Guid EmployeeId;
    }

    public class GetSalaryForEmployeeHandler : IRequestHandler<GetSalaryForEmployee, decimal>
    {
        private readonly IHrisDatabase database;

        public GetSalaryForEmployeeHandler(IHrisDatabase database)
        {
            this.database = database;
        }

        public async Task<decimal> Handle(GetSalaryForEmployee request, CancellationToken token)
        {
            var employee = await database.Employees
                .FirstAsync(o => o.EmployeeId == request.EmployeeId, token);

            return employee.Salary;
        }
    }
}