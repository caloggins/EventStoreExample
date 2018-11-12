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
        public async void ItCreatesEmployees()
        {
            var details = A.Dummy<HireEmployee>();

            var result = await sut.Create(details) as OkResult;

            result.Should().NotBeNull();
        }

        [Fact]
        public async void ItReturnsTheCorrectResultWhenAnEmployeeIsAlreadyHired()
        {
            var request = A.Dummy<HireEmployee>();

            A.CallTo(() => mediator.Send(request, CancellationToken.None))
                .Throws<InvalidOperationException>();

            var result = await sut.Create(request);

            result.Should().BeOfType<BadRequestResult>();
        }
    }
}