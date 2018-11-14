using System;
using System.Threading;
using EmployeesMonitor.Database;
using EmployeesMonitor.EventStoreIntegration;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Xunit;

namespace EmployeesMonitorTests.EventStoreIntegration
{
    public class DispatcherTests
    {
        private readonly IDispatcher sut;
        private object command;

        public DispatcherTests()
        {
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<IRequest<Unit>>._, CancellationToken.None))
                .Invokes(call => command = call.Arguments[0]);
            sut = new Dispatcher(mediator);
        }

        [Fact]
        public async void Dispatch_HandlesEmployeeHired()
        {
            var id = Guid.Parse("b47e9753-c809-4c1b-9703-5b06f8f1f9da");
            var evt = A.Dummy<EmployeeHired>();

            await sut.Dispatch(evt, id);

            var target = command.As<AddEmployee>();
            target.Should().NotBeNull();
            target.Salary.Should().Be(evt.Salary);
            target.EmployeeId.Should().Be(id);
            target.EmployeeName.Should().Be(evt.Name);
        }

        [Fact]
        public async void Dispatch_HandlesTerminated()
        {
            var id = Guid.Parse("b47e9753-c809-4c1b-9703-5b06f8f1f9da");
            var evt = A.Dummy<Terminated>();

            await sut.Dispatch(evt, id);

            var target = command.As<DeleteEmployee>();
            target.Should().NotBeNull();
            target.EmployeeId.Should().Be(id);
        }

        [Fact]
        public async void Dispatch_HandlesSalaryChanged()
        {
            var id = Guid.Parse("b47e9753-c809-4c1b-9703-5b06f8f1f9da");
            var evt = A.Dummy<SalaryChanged>();

            await sut.Dispatch(evt, id);

            var target = command.As<ChangeSalary>();
            target.Should().NotBeNull();
            target.EmployeeId.Should().Be(id);
            target.Salary.Should().Be(evt.Salary);
        }

    }
}