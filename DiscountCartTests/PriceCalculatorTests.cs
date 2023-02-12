using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Dsl;
using DiscountCart.Calculators;
using DiscountCart.Constants;
using DiscountCart.Discounts;
using DiscountCart.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace DiscountCartTests
{
    public class PriceCalculatorTests
    {
        private const double ProductValue = 10.0;
        private readonly PriceCalculator _sut;
        private readonly Mock<IDiscount> _discountMock = new Mock<IDiscount>();
        private readonly IPostprocessComposer<Product> _productFixtureBuilder;

        public PriceCalculatorTests()
        {
            var discountList = new List<IDiscount>
            {
                _discountMock.Object
            };

            _productFixtureBuilder = new Fixture().Build<Product>()
                .With(p => p.Price, ProductValue);

            _sut = new PriceCalculator(discountList);
        }

        [Fact]
        public void CalculatePrice_DoesNotApplyDiscount_IfDiscountDoesntApplyToAnyItems()
        {
            // Arrange
            var items = _productFixtureBuilder.With(p => p.Name, "test").CreateMany(1).ToList();

            var basket = new Fixture().Build<Basket>()
                .With(i => i.Items, items).Create();

            var expected = basket.TotalPrice;

            // Act
            var result = _sut.CalculatePrice(basket);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void CalculatePrice_DoesNotApplyDiscount_IfNotEnoughItemsForDiscountToApply()
        {
            // Arrange
            var items = _productFixtureBuilder.With(p => p.Name, ApplicationConstants.Apple)
                .With(p => p.Quantity, 2).CreateMany(1).ToList();

            var basket = new Fixture().Build<Basket>()
                .With(i => i.Items, items).Create();

            var expected = basket.TotalPrice;

            // Act
            var result = _sut.CalculatePrice(basket);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void CalculatePrice_DoesNotApplyDiscount_WhenDiscountsAreInactive()
        {
            // Arrange
            var items = _productFixtureBuilder.With(p => p.Name, ApplicationConstants.Apple)
                .With(p => p.Quantity, 3).CreateMany(1).ToList();

            var basket = new Fixture().Build<Basket>()
                .With(i => i.Items, items).Create();

            var expected = basket.TotalPrice;

            _discountMock.Setup(x => x.Active).Returns(false);

            // Act
            var result = _sut.CalculatePrice(basket);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void CalculatePrice_AppliesDiscount_WhenConditionsAreValid()
        {
            // Arrange
            var items = _productFixtureBuilder.With(p => p.Name, ApplicationConstants.Apple)
                .With(p => p.Quantity, 3).CreateMany(1).ToList();

            var basket = new Fixture().Build<Basket>()
                .With(i => i.Items, items).Create();

            _discountMock.Setup(x => x.Active).Returns(true);
            _discountMock.Setup(x => x.AppliesTo).Returns(new List<string> { ApplicationConstants.Apple });
            _discountMock.Setup(x => x.CalculateDiscount(It.IsAny<List<Product>>())).Returns(100);

            // Act
            var result = _sut.CalculatePrice(basket);

            // Assert
            _discountMock.Verify(x => x.CalculateDiscount(It.IsAny<List<Product>>()), Times.Once());
        }
    }
}