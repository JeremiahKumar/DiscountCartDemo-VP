using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Dsl;
using DiscountCart.Constants;
using DiscountCart.Discounts;
using DiscountCart.Models;
using FluentAssertions;
using Xunit;

namespace DiscountCartTests
{
    public class TwoForOneDiscountTests
    {
        private const double ProductValue = 10.0;

        private readonly TwoForOneDiscount _sut;
        private readonly IPostprocessComposer<Product> _fixtureBuilder;

        public TwoForOneDiscountTests()
        {
            _sut = new TwoForOneDiscount();
            _fixtureBuilder = new Fixture().Build<Product>()
                .With(p => p.Price, ProductValue);
        }

        [Fact]
        public void CalculateDiscount_CorrectlyCalculates_WhenOnlyFirstProductNeedsDeductions()
        {
            // Arrange
            var appleProduct = _fixtureBuilder
                .With(a => a.Name, ApplicationConstants.Apple)
                .With(p => p.Quantity, 2).Create();

            var orangeProduct = _fixtureBuilder
                .With(a => a.Name, ApplicationConstants.Orange)
                .With(p => p.Quantity, 2).Create();


            // Act
            var resultOne = _sut.CalculateDiscount(new List<Product> { appleProduct, orangeProduct });
            var resultTwo = _sut.CalculateDiscount(new List<Product> { appleProduct });

            // Assert
            resultOne.Should().Be(10.0);
            resultTwo.Should().Be(0);
        }

        [Fact]
        public void CalculateDiscount_CorrectlyCalculates_WhenMultipleProductsNeedDeduction()
        {
            // Arrange
            var appleProduct = _fixtureBuilder
                .With(a => a.Name, ApplicationConstants.Apple)
                .With(p => p.Quantity, 1).Create();

            var orangeProduct = _fixtureBuilder
                .With(a => a.Name, ApplicationConstants.Orange)
                .With(p => p.Quantity, 5).Create();


            // Act
            var resultOne = _sut.CalculateDiscount(new List<Product> { appleProduct, orangeProduct });

            // Assert
            resultOne.Should().Be(20.0);
        }
    }
}