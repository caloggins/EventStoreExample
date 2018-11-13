using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDataLibrary
{
    public class GetEmployees : IRequest<IEnumerable<Employee>>
    {
        
    }

    public class GetEmployeesHandler : IRequestHandler<GetEmployees, IEnumerable<Employee>>
    {
        private readonly IHrisDatabase database;

        public GetEmployeesHandler(IHrisDatabase database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<Employee>> Handle(GetEmployees request, CancellationToken token)
        {
            return await database.Employees
                .Select(o => o)
                .ToListAsync(token);
        }
    }
}