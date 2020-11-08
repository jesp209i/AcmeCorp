using AcmeCorp.Application.Queries;
using AutoFixture;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Tests.Validators
{
    public class GetSubmissionsValidationTest
    {
        private readonly GetSubmissionsValidator _validator;

        public GetSubmissionsValidationTest()
        {
            _validator = new GetSubmissionsValidator();

        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(11)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void InvalidNumbers(int badNumber)
        {
            // Arrange
            var request = new GetSubmissions { Page = badNumber };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Page);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void ValidNumbers(int godNumber)
        {
            // Arrange
            var request = new GetSubmissions { Page = godNumber };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
