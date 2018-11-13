using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests
{
    public class EmployeeTests
    {
        private readonly EmployeeState state;
        private readonly Employee sut;

        public EmployeeTests()
        {
            state = new EmployeeState();
            sut = new Employee(state);
        }

        [Theory]
        [InlineData("96ba0278-1e9d-4b33-8d07-31b3414d7619", "Marky Mark", 50000)]
        [InlineData("94dd2983-2042-45e2-b1e1-507f87517863", "Funky Dude", 40000)]
        public void Hire_HiredEmployeesHaveEvents(Guid id, string name, decimal salary)
        {
            var expected = new EmployeeHired
            {
                EmployeeId = id,
                Name = name,
                Salary = salary,
            };

            sut.Hire(id, name, salary);

            sut.NewEvents.Last()
                .Should().BeEquivalentTo(expected, opt => opt.IncludingFields());
        }

        [Theory]
        [InlineData("200e57ec-cc86-4a75-8a9d-f418dfad1abd", "Marky Mark", 50000)]
        [InlineData("d59a657f-82ce-48c2-9074-f655aa57e4a6", "Funky Dude", 40000)]
        public void Hire_HiredEmployeesHaveTheCorrectDetails(Guid id, string name, decimal salary)
        {
            var expected = new
            {
                Id = id,
                Name = name,
                Salary = salary
            };

            sut.Hire(id, name, salary);

            state.Should().BeEquivalentTo(expected, opt => opt.IncludingFields());
        }

        [Fact]
        public void Hire_ThrowsExceptionWhenAlreadyHired()
        {
            var id = Guid.Parse("bb51a76b-17e0-463b-a64c-54ea0fd52de7");
            sut.Hire(id, "Marky Mark", 50000);

            Action act = () =>
            {
                sut.Hire(id, "Marky Mark", 50000);
            };

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("A person may only be hired once.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Hire_ThrowsExceptionWhenSalaryIsInvalid(decimal salary)
        {
            Action act = () => sut.Hire(Guid.NewGuid(), "Marky Mark", salary);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Negative salaries are not allowed.");
        }

        [Fact]
        public void ChangeSalary_EmployeeMustBeHired()
        {
            Action act = () => sut.ChangeSalary(10000, "");

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("The employee must be hired.");
        }

        [Theory]
        [InlineData(100000, "this is a reason")]
        public void ChangeSalary_UpdatesTheSalary(decimal salary, string reason)
        {
            HireEmployee();

            sut.ChangeSalary(salary, reason);

            state.Salary.Should().Be(salary);
            sut.NewEvents.Last().As<SalaryChanged>()
                .Reason.Should().Be(reason);
        }

        [Fact]
        public void ChangeSalary_FailsWhenSalaryIsNegative()
        {
            HireEmployee();

            Action act = () => sut.ChangeSalary(-1, "");

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Negative salaries are not allowed.");
        }

        [Fact]
        public void Terminate_ThrowExceptionWhenNotHired()
        {
            Action act = () => sut.Terminate("reason");

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("You cannot fire someone you didn't hire!");
        }

        private void HireEmployee()
        {
            sut.Hire(Guid.NewGuid(), "name", 1);
        }
    }
}
