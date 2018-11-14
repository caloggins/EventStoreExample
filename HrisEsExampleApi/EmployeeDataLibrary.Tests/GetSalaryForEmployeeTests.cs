using System;
using System.Threading;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeDataLibrary.Tests
{
    public class GetSalaryForEmployeeTests
    {
        private readonly IHrisDatabase database;
        private readonly IRequestHandler<GetSalaryForEmployee, decimal> sut;

        public GetSalaryForEmployeeTests()
        {
            var options = new DbContextOptionsBuilder<HrisDatabase>()
                .UseInMemoryDatabase("GetSalaryForEmployeeTests")
                .Options;
            database = new HrisDatabase(options);
            var employees = A.CollectionOfDummy<Employee>(5);
            database.Employees.AddRange(employees);
            sut = new GetSalaryForEmployeeHandler(database);
        }

        [Fact]
        public async void Handle_ReturnsTheSalaryForAnEmployee()
        {
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeName = "Mark",
                Salary = 100000m
            };
            database.Employees.Add(employee);
            await database.SaveChangesAsync(CancellationToken.None);

            var request = A.Dummy<GetSalaryForEmployee>();
            request.EmployeeId = employee.EmployeeId;

            var response = await sut.Handle(request, CancellationToken.None);

            response.Should().Be(employee.Salary);
        }
    }
}