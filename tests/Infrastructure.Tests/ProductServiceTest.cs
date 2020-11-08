using AcmeCorp.Infrastructure;
using AcmeCorp.Infrastructure.Interfaces;
using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task IsSerialNumberValid_True()
        {
            // Arrange
            var product = new Fixture().Create<Product>();
            var fetcherMock = new Mock<IProductFetcher>();
            fetcherMock.Setup(x => x.GetProduct(product.SerialNumber)).ReturnsAsync(product);
            var productService = new ProductService(fetcherMock.Object);

            // Act
            var actual = await productService.IsSerialNumberValid(product.SerialNumber);

            // Assert
            actual.Should().BeTrue();
        }
        [Fact]
        public async Task IsSerialNumberValid_False()
        {
            // Arrange
            var serialNumber = "bad Serial";
            Product product = default;
            var fetcherMock = new Mock<IProductFetcher>();
            fetcherMock.Setup(x => x.GetProduct(serialNumber)).ReturnsAsync(product);
            var productService = new ProductService(fetcherMock.Object);

            // Act
            var actual = await productService.IsSerialNumberValid(serialNumber);

            // Assert
            actual.Should().BeFalse();
        }
    }
}
