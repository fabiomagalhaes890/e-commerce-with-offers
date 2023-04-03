using AutoFixture;
using FluentAssertions;
using Klir.TechChallenge.Application.Offers;
using Klir.TechChallenge.Application.Offers.Command;
using KlirTechChallenge.Web.Api.Controllers.Offers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using Xunit;

namespace Klir.TechChallenge.Tests.Web.Api.Controllers.Offers
{
    public class OfferControllerTest
    {
        private readonly OfferController _controller;
        private OfferValueObject _response;

        public OfferControllerTest()
        {
            var fx = new Fixture();
            _response = fx.Create<OfferValueObject>();

            var _mediator = new Mock<IMediator>();
            _mediator.Setup(m => m.Send(It.IsAny<OfferCreateCommand>(), CancellationToken.None)).ReturnsAsync(_response);

            _controller = new OfferController(_mediator.Object);
        }

        [Fact]
        public async void Post_WhenSuccess_ShouldReturns_OkObjectResultWithOfferValueObject()
        {
            //Arrange
            var fx = new Fixture();
            var command = fx.Create<OfferCreateCommand>();
            var expected = new OkObjectResult(_response);

            //Act
            var actual = await _controller.Post(command);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
