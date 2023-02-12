using System.Linq;
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
                .With(p => p.Price, ProductValue)
                .With(p => p.Quantity, 1);
        }

        [Fact]
        public void CalculateDiscount_CorrectlyCalculates()
        {
            var productList = _fixtureBuilder.With(a => a.Name, ApplicationConstants.Apple).CreateMany(2).ToList();
            productList.AddRange(_fixtureBuilder.With(a => a.Name, ApplicationConstants.Orange).CreateMany(2));

            var result = _sut.CalculateDiscount(productList);

            result.Should().Be(10.0);
        }
    }
}