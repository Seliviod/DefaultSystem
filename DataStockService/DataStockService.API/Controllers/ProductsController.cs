using DataStockService.App.Handlers.ProductHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataStockService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {

        [HttpPost("action")]
        public async Task<IActionResult> Create(CreateProductCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
