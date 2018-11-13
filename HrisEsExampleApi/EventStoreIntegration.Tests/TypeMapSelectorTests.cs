using FluentAssertions;
using Xunit;

namespace EventStoreIntegration.Tests
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