using System;
using EmployeesMonitor.BuildTable;
using EmployeesMonitor.EventStoreIntegration;
using FluentAssertions;
using Xunit;

namespace EmployeesMonitorTests
{
    public class StreamExtensionsTests
    {
        [Theory]
        [InlineData("Employee.415f030f-f038-432f-82ab-57e64a49dbbb", "415f030f-f038-432f-82ab-57e64a49dbbb")]
        [InlineData("Employee.eea50143-7436-4c41-9d15-872ef60bcd39", "eea50143-7436-4c41-9d15-872ef60bcd39")]
        public void GetIdFromStream_ReturnsGuid(string stream, Guid id)
        {
            stream.GetIdFromStream().Should().Be(id);
        }
    }
}
