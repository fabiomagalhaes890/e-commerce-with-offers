using Klir.TechChallenge.Application.Services;
using Klir.TechChallenge.Application.Services.Checkouts;
using Microsoft.AspNetCore.Mvc;

namespace KlirTechChallenge.Web.Api.Controllers.Checkouts
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _service;

        public CheckoutController(ICheckoutService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CheckoutValueObject))]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ShoppingCartValueObject shoppingCartValueObject)
        {
            if(shoppingCartValueObject == null 
                || shoppingCartValueObject.Products.Count <= 0 
                || shoppingCartValueObject.UserId <= 0) return BadRequest("Invalid data.");

            var result = _service.Checkout(shoppingCartValueObject);

            if (result == null) return BadRequest();

            return Ok(result);
        }
    }
}
