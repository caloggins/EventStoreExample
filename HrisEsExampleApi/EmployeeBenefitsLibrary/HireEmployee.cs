using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EmployeeBenefitsLibrary
{
    public class HireEmployee : IRequest
    {
        public Guid EmployeeId;
        public string Name;
        public decimal Salary;
    }

    public class HireEmployeeHandler : IRequestHandler<HireEmployee>
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public HireEmployeeHandler(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public async Task<Unit> Handle(HireEmployee request, CancellationToken cancellationToken)
        {
            var employee = await reader.Read<Employee>(request.EmployeeId);

            employee.Hire(
                request.EmployeeId,
                request.Name,
                request.Salary);

            await writer.Write(employee);

            return Unit.Value;
        }
    }
}