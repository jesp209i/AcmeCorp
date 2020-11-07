using AcmeCorp.Application.Commands;
using AcmeCorp.Infrastructure;
using AutoFixture;
using FluentValidation.TestHelper;
using Moq;
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

            _validator = new EnterCompetitionValidator(new Mock<IProductService>().Object);
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
        public void EnterCompetition_NoValidationErrors()
        {
            // Arrange
            var request = _fixture.Build<EnterCompetition>()
                .With(x => x.Email, "test@test.com")
                .With(x=> x.AcceptsTerms, true)
                .With(x => x.ConfirmsCorrectAge, true)
                .Create();

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void EnterCompetition_ValidSerialNumber()
        {
            // Arrange
            var validSerialNumber = "I Am a Serial Number";
            var request = _fixture.Build<EnterCompetition>()
                .With(x => x.SerialNumber, validSerialNumber)
                .Create();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.IsSerialNumberValid(validSerialNumber)).Returns(true);
            var validator = new EnterCompetitionValidator(productServiceMock.Object);

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x=>x.SerialNumber);
        }
        [Fact]
        public void EnterCompetition_BadSerialNumber()
        {
            // Arrange
            var badSerialNumber = "I Am a Serial Number";
            var request = _fixture.Build<EnterCompetition>()
                .With(x => x.SerialNumber, badSerialNumber)
                .Create();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.IsSerialNumberValid(badSerialNumber)).Returns(false);
            var validator = new EnterCompetitionValidator(productServiceMock.Object);

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SerialNumber);
        }
    }
}
