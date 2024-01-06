using E_CommerceApp.Api.Application.Features.Queries.User;
using E_CommerceApp.Common.Models.RequestModels.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace E_CommerceApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> GetUsers([FromBody] CreateUserCommand query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);  
        }

        [HttpPost]
        [Route("GetCart")]
        public async Task<IActionResult> GetCart([FromBody] GetUserShoppingCartDetailQuery query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUserQuery query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
