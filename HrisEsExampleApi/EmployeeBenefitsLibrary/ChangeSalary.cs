using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EmployeeBenefitsLibrary
{
    public class ChangeSalary : IRequest<Unit>
    {
        public Guid EmployeeId;
        public decimal Salary;
        public string Reason;
    }

    public class ChangeSalaryHandler : IRequestHandler<ChangeSalary, Unit>
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public ChangeSalaryHandler(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public async Task<Unit> Handle(ChangeSalary request, CancellationToken cancellationToken)
        {
            var employee = await reader.Read<Employee>(request.EmployeeId);

            employee.ChangeSalary(request.Salary, request.Reason);

            await writer.Write(employee);

            return Unit.Value;
        }
    }
}