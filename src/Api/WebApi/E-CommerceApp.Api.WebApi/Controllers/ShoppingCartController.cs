using System.Text.Json.Serialization;
using E_CommerceApp.Common.Enums;
using E_CommerceApp.Common.Models.RequestModels.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : BaseController
    {
        private readonly IMediator mediator;

        public ShoppingCartController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("AddOrDeleteItemInCart")]
        public async Task<IActionResult> AddItemToCart(AddOrDeleteItemShoppingCartCommand addOrDeleteItemShoppingCartCommand)
        {
            if (addOrDeleteItemShoppingCartCommand.UserId == Guid.Empty)
            {
                addOrDeleteItemShoppingCartCommand.UserId = UserId;
            }
            var result = await mediator.Send(addOrDeleteItemShoppingCartCommand);

            return Ok(result);
        }
    }
}
