using AcmeCorp.Persistence;
using AcmeCorp.Persistence.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
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
    }
}
// Arrange
// Act
// Assert