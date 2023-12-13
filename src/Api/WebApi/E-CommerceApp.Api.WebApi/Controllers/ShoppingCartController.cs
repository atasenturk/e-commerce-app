using System.Text.Json.Serialization;
using E_CommerceApp.Common.Models.RequestModels.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShoppingCartController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("AddItemToCart")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddProductToShoppingCartCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }
    }
}
