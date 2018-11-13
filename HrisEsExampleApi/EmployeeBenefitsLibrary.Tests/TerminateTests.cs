using System;
using System.Threading;
using FakeItEasy;
using MediatR;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests
{
    public class TerminateTests
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IRequestHandler<Terminate, Unit> sut;

        public TerminateTests()
        {
            reader = A.Fake<IReader>();
            writer = A.Fake<IWriter>();
            sut = new TerminateHandler(reader, writer);
        }

        [Fact]
        public async void Handle_ExecutesTheCommand()
        {
            var employee = A.Fake<Employee>();
            var id = Guid.Parse("9131d1bb-f4d2-48c1-bae9-743fe9426f41");
            A.CallTo(() => reader.Read<Employee>(id)).Returns(employee);
            var request = A.Dummy<Terminate>();
            request.EmployeeId = id;

            await sut.Handle(request, CancellationToken.None);

            A.CallTo(() => employee.Terminate(request.Reason)).MustHaveHappened()
                .Then(A.CallTo(() => writer.Write(employee)).MustHaveHappened());
        }
    }
}