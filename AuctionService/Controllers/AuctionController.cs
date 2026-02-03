using AuctionService.Features.Auctions.Commands;
using AuctionService.Features.Auctions.DTOs;
using AuctionService.Features.Auctions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AuctionService.Controllers
{
    [Route("api/auction")]
    public class AuctionController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<AuctionDTO>>> GetAuctions(CancellationToken cancellationToken)
        {
            return HandleResult(await Mediator.Send(new GetAllAuctions.Query { }, cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuction(int id, CancellationToken cancellationToken)
        {
            return HandleResult(await Mediator.Send(new GetAuctionById.Query { Id = id }, cancellationToken));
        }
        [HttpPost]
        public async Task<ActionResult<AuctionDTO>> CreateAuction(CreateAuctionDTO auctionDTO, CancellationToken cancellationToken)
        {
            return HandleResult(await Mediator.Send(new AddAuction.Command { AuctionDTO = auctionDTO }, cancellationToken));
        }
        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateAuction(UpdateAuctionDTO auctionDTO, CancellationToken cancellationToken)
        {
            return HandleResult(await Mediator.Send(new UpdateAuction.Command { AuctionDTO = auctionDTO }, cancellationToken));
        }
        [HttpDelete]
        public async Task<ActionResult<Unit>> DeleteAuction(int id, CancellationToken cancellationToken)
        {
            return HandleResult(await Mediator.Send(new DeleteAuction.Command { Id = id }, cancellationToken));
        }
    }
}
