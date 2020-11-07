using AcmeCorp.Application.Commands;
using AcmeCorp.Infrastructure;
using AcmeCorp.Persistence;
using AutoFixture;
using FluentValidation.TestHelper;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Validators
{
    public class EnterCompetitionValidatorTest
    {
        private readonly Fixture _fixture;
        private readonly EnterCompetitionValidator _validator;

        public EnterCompetitionValidatorTest()
        {
            _fixture = new Fixture();
            _validator = new EnterCompetitionValidator(new Mock<IProductService>().Object, new Mock<ICompetitionRepository>().Object);
        }
        [Fact]
        public void CorrectAgeNotConfirmed()
        {
            // Arrange
            var request = _fixture.Build<EnterCompetition>().Without(x => x.ConfirmsCorrectAge).Create();

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ConfirmsCorrectAge);
        }
        [Fact]
        public void TermsNotAccepted()
        {
            // Arrange
            var request = _fixture.Build<EnterCompetition>().Without(x => x.AcceptsTerms).Create();

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AcceptsTerms);
        }
        [Theory]
        [InlineData("bob bob.dk")]
        [InlineData("name@domain@damiancom")]
        [InlineData("")]
        public void EmailNotValid(string badEmail)
        {
            // Arrange
            var request = _fixture.Build<EnterCompetition>()
                .With(x => x.Email, badEmail).Create();

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
        [Fact]
        public async Task NoValidationErrors()
        {
            // Arrange
            var request = _fixture.Build<EnterCompetition>()
                .With(x => x.Email, "test@test.com")
                .With(x=> x.AcceptsTerms, true)
                .With(x => x.ConfirmsCorrectAge, true)
                .Create();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.IsSerialNumberValid(request.SerialNumber)).Returns(true);
            var competitionRepoMock = new Mock<ICompetitionRepository>();
            competitionRepoMock.Setup(x => x.IsSerialNumberEligible(request.SerialNumber)).ReturnsAsync(true);
            var validator = new EnterCompetitionValidator(productServiceMock.Object, competitionRepoMock.Object);
            
            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void EligiableValidSerialNumber()
        {
            // Arrange
            var validSerialNumber = "I Am a Serial Number";
            var request = _fixture.Build<EnterCompetition>()
                .With(x => x.SerialNumber, validSerialNumber)
                .Create();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.IsSerialNumberValid(validSerialNumber)).Returns(true);
            var competitionRepoMock = new Mock<ICompetitionRepository>();
            competitionRepoMock.Setup(x => x.IsSerialNumberEligible(validSerialNumber)).ReturnsAsync(true);
            var validator = new EnterCompetitionValidator(productServiceMock.Object, competitionRepoMock.Object);

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x=>x.SerialNumber);
        }
        [Fact]
        public void BadSerialNumber()
        {
            // Arrange
            var badSerialNumber = "I Am a Serial Number";
            var request = _fixture.Build<EnterCompetition>()
                .With(x => x.SerialNumber, badSerialNumber)
                .Create();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.IsSerialNumberValid(badSerialNumber)).Returns(false);
            var competitionRepoMock = new Mock<ICompetitionRepository>();
            var validator = new EnterCompetitionValidator(productServiceMock.Object, competitionRepoMock.Object);

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SerialNumber).WithErrorMessage("You must provide a valid serial number to enter competition.");
        }
        [Fact]
        public async Task ValidSerialNumber_NotEligiable()
        {
            // Arrange
            var validSerialNumber = "I Am a Serial Number";
            var request = _fixture.Build<EnterCompetition>()
                .With(x => x.SerialNumber, validSerialNumber)
                .Create();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.IsSerialNumberValid(validSerialNumber)).Returns(true);
            var competitionRepoMock = new Mock<ICompetitionRepository>();
            competitionRepoMock.Setup(x => x.IsSerialNumberEligible(validSerialNumber)).ReturnsAsync(false);
            var validator = new EnterCompetitionValidator(productServiceMock.Object, competitionRepoMock.Object);

            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SerialNumber).WithErrorMessage("Serial number is no longer eligiable to enter draw.");
        }
    }
}
