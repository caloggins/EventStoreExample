using EmployeeBenefitsLibrary.EventStoreIntegration;
using FluentAssertions;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests.EventStoreIntegration
{
    public class TypeMapSelectorTests
    {
        private readonly ITypeMapSelector sut;

        public TypeMapSelectorTests()
        {
            sut = new TypeMapSelector();
        }

        [Fact]
        public void ItReturnsTypeMaps()
        {
            sut.For(null)
                .Should().BeOfType<EmployeeTypeMap>();
        }
    }
}