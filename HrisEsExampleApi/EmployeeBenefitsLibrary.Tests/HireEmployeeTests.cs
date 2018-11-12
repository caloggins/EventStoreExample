using System;
using System.Threading;
using FakeItEasy;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests
{
    public class HireEmployeeTests
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly HireEmployeeHandler sut;

        public HireEmployeeTests()
        {
            reader = A.Fake<IReader>();
            writer = A.Fake<IWriter>();
            sut = new HireEmployeeHandler(reader, writer);
        }

        [Fact]
        public async void ItHiresEmployees()
        {
            var employee = A.Fake<Employee>();
            A.CallTo(() => reader.Read<Employee>(A<Guid>._)).Returns(employee);
            var request = new HireEmployee
            {
                Name = "Marky Mark",
                EmployeeId = Guid.Parse("cc6a3a55-ce2c-49e3-872b-4dfff766726f"),
                Salary = 10000
            };

            await sut.Handle(request, CancellationToken.None);

            A.CallTo(() => employee.Hire(request.EmployeeId, request.Name, request.Salary)).MustHaveHappened()
                .Then(A.CallTo(() => writer.Write(employee)).MustHaveHappened());
        }
    }
}