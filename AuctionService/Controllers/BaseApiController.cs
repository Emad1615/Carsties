using AuctionService.RequestHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        protected IMediator Mediator => _mediator
            ?? HttpContext.RequestServices.GetRequiredService<IMediator>()
            ?? throw new InvalidOperationException("Mediator not found");
        protected ActionResult<T> HandleResult<T>(Result<T> result)
        {
            if (!result.IsSuccess && result.Status == 404)
                return NotFound(new
                {
                    Message = result.Error,
                    result.Status
                });
            if (!result.IsSuccess && result.Status == 400)
                return BadRequest(new
                {
                    Message = result.Error,
                    result.Status
                });
            return Ok(new
            {
                Data = result.Value,
                result.Status
            });
        }

    }
}
