using System;
using FluentAssertions;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests
{
    public class EventTests
    {
        [Fact]
        public void EventsShouldNotHaveEmptyIds()
        {
            var evt = new Event();

            evt.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void AnEventsIdDoesNotChange()
        {
            var evt = new Event();

            evt.Id.Should().Be(evt.Id);
        }

        [Fact]
        public void EventsHaveDifferentIds()
        {
            var a = new Event();
            var b = new Event();

            a.Id.Should().NotBe(b.Id);
        }
    }
}