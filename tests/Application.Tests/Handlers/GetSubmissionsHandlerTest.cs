using AcmeCorp.Application.Queries;
using AcmeCorp.Persistence;
using AcmeCorp.Persistence.Models;
using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Handlers
{
    public class GetSubmissionsHandlerTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICompetitionRepository> _repoMock;

        public GetSubmissionsHandlerTest()
        {
            _fixture = new Fixture();
            _repoMock = new Mock<ICompetitionRepository>();
        }

        [Fact]
        public async Task GetSubmissions()
        {
            // Arrange
            var responses = _fixture.CreateMany<Contestant>(13).ToList();
            var expectedResponses = responses.Select(x => ContestantVM.Create(x));
            var request = new GetSubmissions { Page = 1 };
            _repoMock.Setup(x => x.GetSubmissionsPage(request.Page)).ReturnsAsync(responses);
            var handler = new GetSubmissionsHandler(_repoMock.Object);

            // Act
            var response = await handler.Handle(request);

            // Assert
            response.Submissions.Should().BeEquivalentTo(expectedResponses);
        }
        [Fact]
        public async Task GetSubmissions_CallsDependencyWithCorrectParameter()
        {
            // Arrange
            var responses = _fixture.CreateMany<Contestant>(2).ToList();
            int parameter = default;
            var request = new GetSubmissions { Page = 42 };
            _repoMock.Setup(x => x.GetSubmissionsPage(request.Page))
                .Callback<int>((i)=>parameter = i)
                .ReturnsAsync(responses);
            var handler = new GetSubmissionsHandler(_repoMock.Object);

            // Act
            var response = await handler.Handle(request);

            // Assert
            parameter.Should().Be(request.Page);
        }
    }
}
