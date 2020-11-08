using AcmeCorp.Application.Commands;
using AcmeCorp.WebApi.Controllers;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace WebAPI.Tests
{
    public class CompetitionControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Fixture _fixture;
        public CompetitionControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _fixture = new Fixture();
        }
        [Fact]
        public async Task EnterCompetition_ReturnsStatus200()
        {
            // Arrange
            var expectedStatusCode = (int)HttpStatusCode.OK;
            var request = _fixture.Create<EnterCompetition>();
            _mediatorMock.Setup(x => x.Send(request, default)).ReturnsAsync(new Unit());
            var controller = new CompetitionController(_mediatorMock.Object);
            
            // Act
            var response = await controller.EnterCompetition(request);
            var actualResponse = (OkObjectResult)response;

            // Assert
            actualResponse.StatusCode.Should().Be(expectedStatusCode);
        }
        [Fact]
        public async Task EnterCompetition_MediatorSendsCorrectInput()
        {
            // Arrange
            var request = _fixture.Create<EnterCompetition>();
            EnterCompetition actual = null;
            _mediatorMock.Setup(x => x.Send(request, default))
                .Callback((IRequest<Unit> req, CancellationToken token) => actual = req as EnterCompetition)
                .ReturnsAsync(new Unit());
            var controller = new CompetitionController(_mediatorMock.Object);

            // Act
            var response = await controller.EnterCompetition(request);

            // Assert
            actual.Should().Be(request);
        }
    }
}
