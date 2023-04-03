using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Klir.TechChallenge.Application.Offers.Command;
using Klir.TechChallenge.CrossCutting.Configurations.Mapper;
using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Klir.TechChallenge.Domain.Base.Repositories;
using Klir.TechChallenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading;
using Xunit;

namespace Klir.TechChallenge.Tests.Application.Offers.Queries
{
    public class OfferCommandHandlerTest
    {
        private readonly OfferCommandHandler _commandHandler;
        private readonly Mock<IOfferRepository> _repository;
        private readonly IMapper _mapper;

        private Offer _offer;

        public OfferCommandHandlerTest()
        {
            var options = new DbContextOptionsBuilder<KlirDbContext>().Options;
            var _context = new Mock<KlirDbContext>(options);
            _repository = new Mock<IOfferRepository>();

            var fixture = new Fixture();
            fixture.Customize<Offer>(o => o.With(opt => opt.Id, 0).With(opt => opt.Status, true));
            _offer = fixture.Create<Offer>();
            _repository.Setup(r => r.AddAsync(It.IsAny<Offer>())).ReturnsAsync(_offer);
            _repository.Setup(r => r.GetActivatedProductById(It.IsAny<int>())).Returns<Offer>(null);

            _mapper = new MapperConfiguration(config =>
            {                
                config.AddProfile(new IRequestToEntity());
                config.AddProfile(new EntityToValueObject());
            }).CreateMapper();

            _commandHandler = new OfferCommandHandler(_repository.Object, _mapper);
        }

        [Fact]
        public async void OfferCommandHandler_WhenCreate_ShouldReturns_ObjectValueObject()
        {
            //Arrange
            var expected = new OfferCreateCommand() 
            { 
                Id = _offer.Id, 
                Quantity = _offer.Quantity,
                ProductId = _offer.ProductId,
                Price = _offer.Price,
                Name = _offer.Name,
                Status = _offer.Status                
            };

            //Act
            var actual = await _commandHandler.Handle(expected, CancellationToken.None);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void OfferCommandHandler_WhenUpdate_ShouldReturns_ObjectValueObject()
        {
            //Arrange
            _repository.Setup(r => r.UpdateAsync(It.IsAny<Offer>())).ReturnsAsync(_offer);
            var expected = new OfferCreateCommand()
            {
                Id = _offer.Id,
                Quantity = _offer.Quantity,
                ProductId = _offer.ProductId,
                Price = _offer.Price,
                Name = _offer.Name,
                Status = _offer.Status
            };

            //Act
            var actual = await _commandHandler.Handle(expected, CancellationToken.None);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
