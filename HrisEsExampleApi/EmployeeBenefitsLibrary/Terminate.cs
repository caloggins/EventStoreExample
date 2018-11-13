using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EmployeeBenefitsLibrary
{
    public class Terminate : IRequest<Unit>
    {
        public Guid EmployeeId;
        public string Reason;
    }

    public class TerminateHandler : IRequestHandler<Terminate, Unit>
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public TerminateHandler(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public async Task<Unit> Handle(Terminate request, CancellationToken cancellationToken)
        {
            var employee = await reader.Read<Employee>(request.EmployeeId);

            employee.Terminate(request.Reason);

            await writer.Write(employee);

            return Unit.Value;
        }
    }
}