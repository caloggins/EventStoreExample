using System.Collections.Generic;
using System.Threading;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Xunit;

namespace EmployeeDataLibrary.Tests
{
    public class GetEmployeesTests
    {
        private readonly IRequestHandler<GetEmployees, IEnumerable<Employee>> sut;
        private IList<Employee> employees;
        private readonly IHrisDatabase database;

        public GetEmployeesTests()
        {
            database = A.Fake<IHrisDatabase>();
            sut = new GetEmployeesHandler(database);
        }

        [Fact]
        public async void Handle_ReturnsListOfEmployees()
        {
            employees = A.CollectionOfDummy<Employee>(5);
            A.CallTo(() => database.Employees).ReturnsLazily(
                () => TestDbSet.CreateFromCollection(employees));
            var request = A.Dummy<GetEmployees>();

            var result = await sut.Handle(request, CancellationToken.None);

            result.Should().BeEquivalentTo(employees);
        }
    }
}