using System.Collections.Generic;
using System.Threading;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var options = new DbContextOptionsBuilder<HrisDatabase>()
                .UseInMemoryDatabase("mydb")
                .Options;

            database = new HrisDatabase(options);
            sut = new GetEmployeesHandler(database);
        }

        [Fact]
        public async void Handle_ReturnsListOfEmployees()
        {
            employees = A.CollectionOfDummy<Employee>(5);
            database.Employees.AddRange(employees);
            await database.SaveChangesAsync(CancellationToken.None);

            var request = A.Dummy<GetEmployees>();

            var result = await sut.Handle(request, CancellationToken.None);

            result.Should().BeEquivalentTo(employees);
        }
    }
}