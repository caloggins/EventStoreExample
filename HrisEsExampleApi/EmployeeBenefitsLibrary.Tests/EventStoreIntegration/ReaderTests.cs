using System;
using EmployeeBenefitsLibrary.EventStoreIntegration;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests.EventStoreIntegration
{
    public class ReaderTests
    {
        private readonly IStreamLoader loader;
        private readonly ITypeMapSelector selector;
        private readonly IReader sut;

        public ReaderTests()
        {
            loader = A.Fake<IStreamLoader>();
            selector = A.Fake<ITypeMapSelector>();
            sut = new Reader(loader, selector);
        }

        [Theory]
        [InlineData("7b60e726-a792-448d-ade7-bbfe94c1b827", "Employee.7b60e726-a792-448d-ade7-bbfe94c1b827")]
        [InlineData("cd114e65-c287-443b-a6e4-8a30223a23cf", "Employee.cd114e65-c287-443b-a6e4-8a30223a23cf")]
        public async void ItReturnsAnAggregate(Guid id, string stream)
        {
            var map = new EmployeeTypeMap();
            A.CallTo(() => selector.For(typeof(Employee)))
                .Returns(map);
            var events = A.CollectionOfDummy<EmployeeHired>(1);
            A.CallTo(() => loader.LoadEvents(
                    stream, map))
                .Returns(events);

            var employee = await sut.Read<Employee>(id);

            employee.Should().NotBeNull();
            employee.Version.Should().Be(1);
        }
    }
}