using System;
using System.Threading;
using FakeItEasy;
using MediatR;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests
{
    public class ChangeSalaryTests
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IRequestHandler<ChangeSalary, Unit> sut;

        public ChangeSalaryTests()
        {
            reader = A.Fake<IReader>();
            writer = A.Fake<IWriter>();
            sut = new ChangeSalaryHandler(reader, writer);
        }

        [Fact]
        public async void Handle_CallsMethodOnAggregate()
        {
            var employeeId = Guid.Parse("5d857127-2629-4515-9c58-706a0555479a");
            var request = A.Dummy<ChangeSalary>();
            request.EmployeeId = employeeId;
            var employee = A.Fake<Employee>();
            A.CallTo(() => reader.Read<Employee>(employeeId))
                .Returns(employee);

            await sut.Handle(request, CancellationToken.None);

            A.CallTo(() => employee.ChangeSalary(request.Salary, request.Reason)).MustHaveHappened()
                .Then(A.CallTo(() => writer.Write(employee)).MustHaveHappened());
        }
    }
}