using System;
using System.Threading;
using EmployeeBenefitsLibrary;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace HrisEsExampleApiHost.Tests
{
    public class EmployeesControllerTests
    {
        private readonly IMediator mediator;
        private readonly EmployeesController sut;

        public EmployeesControllerTests()
        {
            mediator = A.Fake<IMediator>();
            sut = new EmployeesController(mediator);
        }

        [Fact]
        public void GetReturnsValue()
        {
            var result = sut.Employees();

            result.Value.Should().Be("Hello, world.");
        }

        [Fact]
        public async void Hire_ReturnsTheCorrectResult()
        {
            var details = A.Dummy<HireEmployee>();

            var result = await sut.Hire(details) as OkResult;

            result.Should().NotBeNull();
        }

        [Fact]
        public async void Hire_DispatchesTheCommand()
        {
            var request = A.Dummy<HireEmployee>();

            await sut.Hire(request);

            A.CallTo(() => mediator.Send(request, CancellationToken.None)).MustHaveHappened();
        }

        [Fact]
        public async void Hire_ReturnsCorrectResultWhenThereIsAnError()
        {
            var request = A.Dummy<HireEmployee>();
            A.CallTo(() => mediator.Send(request, CancellationToken.None))
                .Throws<InvalidOperationException>();

            var result = await sut.Hire(request);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void SetSalary_ReturnsAppropriateResponseCodeAsync()
        {
            var id = Guid.Parse("7152b7b1-e10c-444e-9606-413eb46bf207");
            var request = ValidChangeSalaryRequest();

            var result = await sut.SetSalary(id, request);

            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void SetSalary_DispatchesTheCommand()
        {
            var id = Guid.Parse("7152b7b1-e10c-444e-9606-413eb46bf207");
            var request = ValidChangeSalaryRequest();

            await sut.SetSalary(id, request);

            A.CallTo(() => mediator.Send(request, CancellationToken.None)).MustHaveHappened();
            request.EmployeeId.Should().Be(id);
        }

        private ChangeSalary ValidChangeSalaryRequest()
        {
            return new ChangeSalary
            {
                Salary = 10000,
                Reason = "Mark is a nice guy."
            };
        }
    }
}