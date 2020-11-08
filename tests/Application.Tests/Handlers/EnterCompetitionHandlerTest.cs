using AcmeCorp.Application.Commands;
using AcmeCorp.Persistence;
using AcmeCorp.Persistence.Models;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Handlers
{
    public class EnterCompetitionHandlerTest
    { 
        private readonly Fixture _fixture;

        public EnterCompetitionHandlerTest()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public async Task EnterCompetition_ContestantRelayedThrough()
        {
            // Arrange
            Contestant actualContestant = null;
            var request = _fixture.Create<EnterCompetition>();
            var expectedContestant = Contestant.Create(request.FirstName, request.LastName, request.Email, request.SerialNumber, request.ConfirmsCorrectAge, request.AcceptsTerms);
            var repoMock = new Mock<ICompetitionRepository>();
            repoMock.Setup(x => x.AddContestant(It.IsAny<Contestant>()))
                .Callback((Contestant contestant) => actualContestant = contestant).Returns(Task.CompletedTask);
            var handler = new EnterCompetitionHandler(repoMock.Object);

            // Act
            var response = await handler.Handle(request);

            // Assert
            actualContestant
                .Should().BeEquivalentTo(expectedContestant, options => options.Excluding(x=>x.CreatedAt));
            actualContestant.CreatedAt.Should().BeCloseTo(expectedContestant.CreatedAt, 1.Seconds());
        }
    }
}
