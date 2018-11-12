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
        public void HiredEmployeesHaveEvents(Guid id, string name, decimal salary)
        {
            var expected = new EmployeeHired
            {
                EmployeeId = id,
                Name = name,
                Salary = salary,
            };

            sut.Hire(id, name, salary);

            sut.NewEvents.Last()
                .Should().BeEquivalentTo(expected, opt =>
                {
                    return opt.IncludingFields().Excluding(o => o.Id);
                });
        }

        [Theory]
        [InlineData("200e57ec-cc86-4a75-8a9d-f418dfad1abd", "Marky Mark", 50000)]
        [InlineData("d59a657f-82ce-48c2-9074-f655aa57e4a6", "Funky Dude", 40000)]
        public void HiredEmployeesHaveTheCorrectDetails(Guid id, string name, decimal salary)
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
        public void EmployeesCanOnlyBeHiredOnce()
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
    }
}
