using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests
{
    public class AggregateTests
    {
        private readonly Aggregate sut;

        public AggregateTests()
        {
            sut = A.Fake<Aggregate>();
        }

        [Fact]
        public void ItLoadsEvents()
        {
            var events = A.CollectionOfDummy<Event>(5);

            sut.LoadHistory(events);

            foreach (var evt in events)
                A.CallTo(() => sut.State.ApplyEvent(evt)).MustHaveHappenedOnceExactly();
            sut.NewEvents.Should().BeEmpty();
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        public void TheAggregateVersionShouldMatchItsState(int version)
        {
            sut.State.Version = version;

            sut.Version.Should().Be(version);
        }


        [Fact(Skip = "Not yet implemented.")]
        public void ItAppliesEvents()
        {
            // todo, test applying new events
        }
    }
}