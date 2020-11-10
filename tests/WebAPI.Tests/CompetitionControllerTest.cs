using AcmeCorp.Application.Commands;
using AcmeCorp.Application.Queries;
using AcmeCorp.WebApi.Controllers;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
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
        [Fact]
        public async Task GetSubmissions_HasNoSubmissions_Produces404()
        {
            // Arrange
            var expectedStatusCode = (int)HttpStatusCode.NotFound;
            var request = new GetSubmissions { Page = 1 };
            var mediatorResponse = new GetSubmissionsResponse(request.Page,1);
            _mediatorMock.Setup(x => x.Send(request, default)).ReturnsAsync(mediatorResponse);
            var controller = new CompetitionController(_mediatorMock.Object);

            // Act
            var response = await controller.GetSubmissions(request);
            var actualResponse = response as NotFoundResult;

            // Assert
            actualResponse.StatusCode.Should().Be(expectedStatusCode);
        }
        [Fact]
        public async Task GetSubmissions_HasSubmissions_Produces200()
        {
            // Arrange
            var expectedStatusCode = (int)HttpStatusCode.OK;
            var request = new GetSubmissions { Page = 1 };
            var submissions = _fixture.CreateMany<ContestantVM>(3).ToList();
            var mediatorResponse = new GetSubmissionsResponse(request.Page,1) { Submissions = submissions};
            _mediatorMock.Setup(x => x.Send(request, default)).ReturnsAsync(mediatorResponse);
            var controller = new CompetitionController(_mediatorMock.Object);

            // Act
            var response = await controller.GetSubmissions(request);
            var actualResponse = response as OkObjectResult;

            // Assert
            actualResponse.StatusCode.Should().Be(expectedStatusCode);
        }
    }
}
