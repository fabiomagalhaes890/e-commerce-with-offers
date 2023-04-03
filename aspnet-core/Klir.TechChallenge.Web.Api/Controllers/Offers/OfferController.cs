using Klir.TechChallenge.Application.Offers;
using Klir.TechChallenge.Application.Offers.Command;
using Klir.TechChallenge.Application.Offers.Delete;
using Klir.TechChallenge.Application.Offers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlirTechChallenge.Web.Api.Controllers.Offers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(OfferValueObject))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] OfferCreateCommand command)
        {
            if(command == null 
                || command.Quantity <= 0 
                || command.Price <= 0) return BadRequest("Invalid data.");

            var result = await _mediator.Send(command);

            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(OfferValueObject))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody] OfferUpdateCommand command)
        {
            if (command == null
                || command.Quantity <= 0
                || command.Price <= 0) return BadRequest("Invalid data.");

            var result = await _mediator.Send(command);

            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpGet("product-id/{productId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OfferValueObject>))]
        public async Task<IActionResult> Get(int productId)
        {
            var result = await _mediator.Send(new OfferQueryByProductId() { ProductId = productId });
            return Ok(result);
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new OfferDeleteCommand() { Id = id });
            return NoContent();
        }
    }
}
