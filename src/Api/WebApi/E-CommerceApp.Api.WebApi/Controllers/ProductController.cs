using E_CommerceApp.Api.Application.Features.Queries.Product;
using E_CommerceApp.Api.Application.Features.Queries.User;
using E_CommerceApp.Common.Models.RequestModels.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetProductsQuery();
            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
