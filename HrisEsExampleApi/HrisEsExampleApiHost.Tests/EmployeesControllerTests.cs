using FluentAssertions;
using Xunit;

namespace HrisEsExampleApiHost.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public void GetReturnsValue()
        {
            var sut = new EmployeesController();

            var result = sut.Employees();

            result.Value.Should().Be("Hello, world.");
        }
    }
}