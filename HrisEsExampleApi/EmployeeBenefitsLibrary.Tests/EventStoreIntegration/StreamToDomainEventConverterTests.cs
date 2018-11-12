using System.Linq;
using System.Text;
using EmployeeBenefitsLibrary.EventStoreIntegration;
using FakeItEasy;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests.EventStoreIntegration
{
    public class StreamToDomainEventConverterTests
    {
        private readonly IConverter sut;

        public StreamToDomainEventConverterTests()
        {
            sut = new StreamToDomainEventConverter();
        }

        [Fact]
        public void ItConvertsLoadedEvents()
        {
            var type = new EmployeeTypeMap()
                .Single(o => o.type == typeof(EmployeeHired)).name;
            var evt = A.Dummy<EmployeeHired>();
            var json = JsonConvert.SerializeObject(evt);
            var bytes = Encoding.UTF8.GetBytes(json);
            var loaded = new LoadedEvent
            {
                Type = type,
                Data = bytes
            };

            var result = sut.ConvertReadToDomain(new EmployeeTypeMap(), loaded);

            result.Should().BeEquivalentTo(evt, opt => opt.Excluding(o => o.Id));
        }
    }
}