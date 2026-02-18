using AuctionService.Features.Auctions.Commands;
using AuctionService.Features.Auctions.DTOs;
using AuctionService.Features.Auctions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AuctionService.Controllers
{
    [Route("api/auction")]
    public class AuctionController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<AuctionDTO>>> GetAuctions(string date, CancellationToken cancellationToken)
        {
            return HandleResult(await Mediator.Send(new GetAllAuctions.Query { Date = date }, cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuction(int id, CancellationToken cancellationToken)
        {
            return HandleResult(await Mediator.Send(new GetAuctionById.Query { Id = id }, cancellationToken));
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AuctionDTO>> CreateAuction(CreateAuctionDTO auctionDTO, CancellationToken cancellationToken)
        {
            auctionDTO.Seller=User.Identity.Name;
            auctionDTO.CreatedBy= User.FindFirst("sub").Value;
            return HandleResult(await Mediator.Send(new AddAuction.Command { AuctionDTO = auctionDTO }, cancellationToken));
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateAuction(UpdateAuctionDTO auctionDTO, CancellationToken cancellationToken)
        {
            auctionDTO.ModifiedBy = User.FindFirst("sub").Value;
            return HandleResult(await Mediator.Send(new UpdateAuction.Command { AuctionDTO = auctionDTO }, cancellationToken));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAuction(int id, CancellationToken cancellationToken)
        {
            return HandleResult(await Mediator.Send(new DeleteAuction.Command { Id = id, DeletedBy = User.FindFirst("sub").Value }, cancellationToken));
        }
    }
}
