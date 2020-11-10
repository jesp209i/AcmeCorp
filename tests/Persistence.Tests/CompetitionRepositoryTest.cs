using AcmeCorp.Persistence;
using AcmeCorp.Persistence.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Persistence.Tests
{
    public class CompetitionRepositoryTest : IClassFixture<TestFixture>
    {
        private readonly TestFixture _testFixture;
        private readonly Fixture _fixture;
        private readonly ICompetitionRepository _competitionRepository;

        public CompetitionRepositoryTest(TestFixture testFixture)
        {
            _testFixture = testFixture;
            _fixture = new Fixture();
            _competitionRepository = new CompetitionRepository(_testFixture.DbContext);
        }
        [Fact]
        public async Task AddContestant()
        {
            // Arrange
            var contestant = _fixture.Create<Contestant>();

            // Act
            await _competitionRepository.AddContestant(contestant);
            var actual = await _testFixture.DbContext.Contestants.ContainsAsync(contestant);
            
            // Assert
            actual.Should().BeTrue();
        }
        [Theory]
        [InlineData("123456", false)]
        [InlineData("654321", true)]
        public async Task IsSerialNumberEligiable(string serial, bool expected)
        {
            // Arrange
            var existingContestant = _fixture.Build<Contestant>().With(x => x.SerialNumber, "123456").Create();
            _testFixture.DbContext.Contestants.Add(existingContestant);
            await _testFixture.DbContext.SaveChangesAsync();

            // Act
            var actual = await _competitionRepository.IsSerialNumberEligible(serial);

            // Assert
            actual.Should().Be(expected);
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(10, 1)]
        [InlineData(20, 2)]
        [InlineData(21, 3)]
        [InlineData(29, 3)]
        [InlineData(30, 3)]
        [InlineData(31, 4)]
        public async Task GetMaxPage_CorrectPage(int numberOfEntries, int expectedMaxPage)
        {
            // Arrange
            var _options = new DbContextOptionsBuilder<AcmeCorpContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            AcmeCorpContext DbContext = new AcmeCorpContext(_options);
            _fixture.Customize<Contestant>(c => c.Without(c => c.Id));
            var contestants = _fixture.CreateMany<Contestant>(numberOfEntries);
            DbContext.Contestants.AddRange(contestants);
            await DbContext.SaveChangesAsync();
            
            var competitionRepository = new CompetitionRepository(DbContext);
            // Act
            var actual = await competitionRepository.MaxPageCount();
            // Assert
            actual.Should().Be(expectedMaxPage);
        }
    }
}
// Arrange
// Act
// Assert