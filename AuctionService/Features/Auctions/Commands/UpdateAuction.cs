using AuctionService.Data;
using AuctionService.Features.Auctions.DTOs;
using MediatR;

namespace AuctionService.Features.Auctions.Commands
{
    public class UpdateAuction
    {
        public class Command : IRequest<Unit>
        {
            public UpdateAuctionDTO AuctionDTO { get; set; }
        }
        public class Handler(AuctionDbContext context) : IRequestHandler<Command, Unit>
        {
            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
