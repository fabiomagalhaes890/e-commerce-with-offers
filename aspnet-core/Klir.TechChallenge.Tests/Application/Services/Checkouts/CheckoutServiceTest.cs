using AutoFixture;
using FluentAssertions;
using Klir.TechChallenge.Application.Services;
using Klir.TechChallenge.Application.Services.Checkouts;
using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Klir.TechChallenge.Domain.Base.Repositories;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Klir.TechChallenge.Tests.Application.Services.Checkouts
{
    public class CheckoutServiceTest
    {
        private readonly ICheckoutService _service;
        private readonly Mock<IOfferRepository> _repository;

        private Offer _offer_1;
        private Offer _offer_2;

        public CheckoutServiceTest()
        {
            var fixture = new Fixture();
            fixture.Customize<Offer>(f => f.With(opt => opt.Price, 20m).With(opt => opt.Quantity, 2).With(opt => opt.ProductId, 1));
            _offer_1 = fixture.Create<Offer>();
            fixture.Customize<Offer>(f => f.With(opt => opt.Price, 10m).With(opt => opt.Quantity, 3).With(opt => opt.ProductId, 2));
            _offer_2 = fixture.Create<Offer>();

            _repository = new Mock<IOfferRepository>();            
            
            _service = new CheckoutService(_repository.Object);
        }

        [Fact]
        public void Checkout_WithoutDiscount_ShouldReturns_CheckoutWithoutDiscount()
        {
            var expected = new CheckoutValueObject
            {
                Discount = 0,
                Total = 10,
                TotalWithDiscount = 10
            };

            var shoppingCart = new ShoppingCartValueObject
            {
                Products = new List<ProductCheckoutValueObject>()
                {
                    new ProductCheckoutValueObject
                    {
                        Count = 1,
                        Product = new ProductValueObject { Id = 1, Name = "Product A", Price = 10 },
                        TotalPrice = 10
                    }
                },
                UserId = 2
            };

            //Act
            var actual = _service.Checkout(shoppingCart);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(20, 40, 20, 2, 20)]
        [InlineData(20, 60, 40, 3, 20)]
        [InlineData(40, 80, 40, 4, 20)]
        public void Checkout_WithDiscountBuy1Get1Free_ShouldReturns_CheckoutWithDiscountsOk(
            decimal discount,
            decimal total,
            decimal totalWithDiscount,
            int count,
            decimal price)
        {
            //Arrange
            _repository.Setup(x => x.GetActivatedProductById(It.IsAny<int>())).Returns(_offer_1);
            var expected = new CheckoutValueObject
            {
                Discount = discount,
                Total = total,
                TotalWithDiscount = totalWithDiscount
            };

            var shoppingCart = new ShoppingCartValueObject
            {
                Products = new List<ProductCheckoutValueObject>() 
                {
                    new ProductCheckoutValueObject
                    {
                        Count = count,
                        Product = new ProductValueObject { Id = 1, Name = "Product A", Price = price },
                        TotalPrice = total
                    }
                },
                UserId = 2
            };

            //Act
            var actual = _service.Checkout(shoppingCart);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(20, 50, 30, 2, 20)]
        [InlineData(20, 70, 50, 3, 20)]
        [InlineData(40, 90, 50, 4, 20)]
        public void Checkout_WithDiscountBuy1Get1Free_And_AnotherProduct_ShouldReturns_CheckoutWithDiscountsOk(
            decimal discount,
            decimal total,
            decimal totalWithDiscount,
            int count,
            decimal price)
        {
            //Arrange
            _repository.Setup(x => x.GetActivatedProductById(1)).Returns(_offer_1);
            _repository.Setup(x => x.GetActivatedProductById(2)).Returns<Offer>(null);

            var expected = new CheckoutValueObject
            {
                Discount = discount,
                Total = total,
                TotalWithDiscount = totalWithDiscount
            };

            var shoppingCart = new ShoppingCartValueObject
            {
                Products = new List<ProductCheckoutValueObject>()
                {
                    new ProductCheckoutValueObject
                    {
                        Count = count,
                        Product = new ProductValueObject { Id = 1, Name = "Product A", Price = price },
                        TotalPrice = total
                    },
                    new ProductCheckoutValueObject
                    {
                        Count = 1,
                        Product = new ProductValueObject { Id = 2, Name = "Product B", Price = 10 },
                        TotalPrice = 10
                    }
                },
                UserId = 2
            };

            //Act
            var actual = _service.Checkout(shoppingCart);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(2, 12, 10, 3, 4)]
        [InlineData(2, 16, 14, 4, 4)]
        [InlineData(2, 20, 18, 5, 4)]
        [InlineData(4, 24, 20, 6, 4)]
        public void Checkout_WithDiscount3For10Euro_ShouldReturns_CheckoutWithDiscountsOk(
            decimal discount, 
            decimal total, 
            decimal totalWithDiscount, 
            int count, 
            decimal price)
        {
            //Arrange
            _repository.Setup(x => x.GetActivatedProductById(It.IsAny<int>())).Returns(_offer_2);
            var expected = new CheckoutValueObject
            {
                Discount = discount,
                Total = total,
                TotalWithDiscount = totalWithDiscount
            };

            var shoppingCart = new ShoppingCartValueObject
            {
                Products = new List<ProductCheckoutValueObject>()
                {
                    new ProductCheckoutValueObject
                    {
                        Count = count,
                        Product = new ProductValueObject { Id = 2, Name = "Product B", Price = price },
                        TotalPrice = total
                    }
                },
                UserId = 2
            };

            //Act
            var actual = _service.Checkout(shoppingCart);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(2, 22, 20, 3, 4)]
        [InlineData(2, 26, 24, 4, 4)]
        [InlineData(2, 30, 28, 5, 4)]
        [InlineData(4, 34, 30, 6, 4)]
        public void Checkout_WithDiscount3For10Euro_And_AnotherProduct_ShouldReturns_CheckoutWithDiscountsOk(
            decimal discount,
            decimal total,
            decimal totalWithDiscount,
            int count,
            decimal price)
        {
            //Arrange
            _repository.Setup(x => x.GetActivatedProductById(2)).Returns(_offer_2);
            var expected = new CheckoutValueObject
            {
                Discount = discount,
                Total = total,
                TotalWithDiscount = totalWithDiscount
            };

            var shoppingCart = new ShoppingCartValueObject
            {
                Products = new List<ProductCheckoutValueObject>()
                {
                    new ProductCheckoutValueObject
                    {
                        Count = 1,
                        Product = new ProductValueObject { Id = 1, Name = "Product A", Price = 10 },
                        TotalPrice = 10
                    },
                    new ProductCheckoutValueObject
                    {
                        Count = count,
                        Product = new ProductValueObject { Id = 2, Name = "Product B", Price = price },
                        TotalPrice = total
                    }
                },
                UserId = 2
            };

            //Act
            var actual = _service.Checkout(shoppingCart);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Checkout_WithDiscount3For10Euro_And_AnotherProductWithOffer_ShouldReturns_CheckoutWithDiscountsOk()
        {
            //Arrange
            _repository.Setup(x => x.GetActivatedProductById(1)).Returns(_offer_1);
            _repository.Setup(x => x.GetActivatedProductById(2)).Returns(_offer_2);
            var expected = new CheckoutValueObject
            {
                Discount = 20,
                Total = 44,
                TotalWithDiscount = 24
            };

            var shoppingCart = new ShoppingCartValueObject
            {
                Products = new List<ProductCheckoutValueObject>()
                {
                    new ProductCheckoutValueObject
                    {
                        Count = 2,
                        Product = new ProductValueObject { Id = 1, Name = "Product A", Price = 20 },
                        TotalPrice = 40
                    },
                    new ProductCheckoutValueObject
                    {
                        Count = 1,
                        Product = new ProductValueObject { Id = 2, Name = "Product B", Price = 4 },
                        TotalPrice = 4
                    }
                },
                UserId = 2
            };

            //Act
            var actual = _service.Checkout(shoppingCart);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Checkout_WithDiscount3For10Euro_And_AnotherProductWithBuy1Get1Free_ShouldReturns_CheckoutWithDiscountsOk()
        {
            //Arrange
            _repository.Setup(x => x.GetActivatedProductById(1)).Returns(_offer_1);
            _repository.Setup(x => x.GetActivatedProductById(2)).Returns(_offer_2);
            var expected = new CheckoutValueObject
            {
                Discount = 22,
                Total = 52,
                TotalWithDiscount = 30
            };

            var shoppingCart = new ShoppingCartValueObject
            {
                Products = new List<ProductCheckoutValueObject>()
                {
                    new ProductCheckoutValueObject
                    {
                        Count = 2,
                        Product = new ProductValueObject { Id = 1, Name = "Product A", Price = 20 },
                        TotalPrice = 40
                    },
                    new ProductCheckoutValueObject
                    {
                        Count = 3,
                        Product = new ProductValueObject { Id = 2, Name = "Product B", Price = 4 },
                        TotalPrice = 12
                    }
                },
                UserId = 2
            };

            //Act
            var actual = _service.Checkout(shoppingCart);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
