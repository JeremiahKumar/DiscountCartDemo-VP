using System;
using System.Linq;
using AutoFixture;
using AutoFixture.Dsl;
using DiscountCart.Constants;
using DiscountCart.Models;
using FluentAssertions;
using Xunit;

namespace DiscountCartTests
{
    public class BasketTests
    {
        private const double ProductValue = 10.0;
        private readonly Basket _sut;
        private readonly IPostprocessComposer<Product> _fixtureBuilder;

        public BasketTests()
        {
            _sut = new Basket();
            _fixtureBuilder = new Fixture().Build<Product>()
                .With(a => a.Name, ApplicationConstants.Apple)
                .With(p => p.Price, ProductValue)
                .With(p => p.Quantity, 1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(999)]
        public void AddProduct_AddsItems_WithCorrectQuantitiesAndPrices(int quantity)
        {
            // Arrange
            var products = _fixtureBuilder.CreateMany(quantity).ToList();
            
            // Act
            foreach (var product in products)
            {
                _sut.AddProduct(product);
            }

            // Assert
            _sut.Items.Should().NotBeEmpty()
                .And.HaveCount(1);
            _sut.TotalPrice.Should().Be(quantity * ProductValue);
        }
        
        [Theory]
        [InlineData(40,39)]
        [InlineData(2,1)]
        public void RemoveProduct_DecrementsItemsCorrectly_WhenResultIsNonZero(int quantity, int expected)
        {
            // Arrange
            var products = _fixtureBuilder.CreateMany(quantity).ToList();
            
            foreach (var product in products)
            {
                _sut.AddProduct(product);
            }

            // Act
            _sut.RemoveProduct(products[1]);
            var result = _sut.Items.Find(p => p.Name == ApplicationConstants.Apple);

            // Assert
            result.Quantity.Should().Be(expected);
            _sut.TotalPrice.Should().Be(expected * ProductValue);
        }
        
        [Fact]
        public void RemoveProduct_RemovesItemsCorrectly_WhenResultZero()
        {
            // Arrange
            var product = _fixtureBuilder.Create();
            product.Quantity = 1;
            _sut.TotalPrice= 10;
            _sut.Items.Add(product);
            
            // Act
            _sut.RemoveProduct(product);

            // Assert
            _sut.Items.Should().BeEmpty();
            _sut.TotalPrice.Should().Be(0);
        }
        
        [Fact]
        public void RemoveProduct_DoesNothing_WhenItemIsNotInBasket()
        {
            // Arrange
            var product = _fixtureBuilder.Create();

            // Act
            Action act = () => _sut.RemoveProduct(product);

            // Assert
            act.Should().NotThrow();
            _sut.Items.Should().BeEmpty();
            _sut.TotalPrice.Should().Be(0);
        }
        
        [Fact]
        public void AddOrRemove_ThrowsException_WhenProductQuantityIsOverOne()
        {
            // Arrange
            var product = _fixtureBuilder.Create();
            product.Quantity = 10;

            Action act = () => _sut.AddProduct(product);
            Action actTwo = () => _sut.RemoveProduct(product);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(ExceptionMessages.MoreThanOneQuantityException);
            actTwo.Should().Throw<ArgumentException>()
                .WithMessage(ExceptionMessages.MoreThanOneQuantityException);
        }
    }
}