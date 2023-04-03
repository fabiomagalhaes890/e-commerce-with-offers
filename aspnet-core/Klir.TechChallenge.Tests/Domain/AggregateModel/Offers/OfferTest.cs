using AutoFixture;
using FluentAssertions;
using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Xunit;

namespace Klir.TechChallenge.Tests.Domain.AggregateModel.Offers
{
    public class OfferTest
    {
        [Theory]
        [InlineData(2, 1, 20, 20, 0)]
        [InlineData(2, 2, 20, 20, 20)]
        [InlineData(2, 3, 20, 20, 20)]
        [InlineData(2, 4, 20, 20, 40)]
        [InlineData(3, 1, 4, 10, 0)]
        [InlineData(3, 2, 4, 10, 0)]
        [InlineData(3, 3, 4, 10, 2)]
        [InlineData(3, 4, 4, 10, 2)]
        [InlineData(3, 5, 4, 10, 2)]
        [InlineData(3, 6, 4, 10, 4)]
        public void CalculateDiscount_WhenOfferBuy1Get1Free_ShouldReturns_DiscountOk(int offerQuantity, int quantity, decimal price, decimal promoPrice, decimal discount)
        {
            //Arrange
            var fx = new Fixture();
            fx.Customize<Offer>(o => o.With(opt => opt.Quantity, offerQuantity).With(opt => opt.Price, promoPrice));
            var expected = fx.Create<Offer>();

            //Act
            var actual = expected.CalculateDiscount(quantity, price);

            //Assert
            actual.Should().Be(discount);
        }
    }
}
