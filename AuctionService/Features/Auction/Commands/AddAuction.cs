using AuctionService.Data;
using AuctionService.Features.Auction.DTOs;
using MediatR;

namespace AuctionService.Features.Auction.Commands
{
    public class AddAuction
    {
        public class Command : IRequest<AuctionDTO>
        {
            public CreateAuctionDTO AuctionDTO { get; set; }
        }
        public class Handler(AuctionDbContext context) : IRequestHandler<Command, AuctionDTO>
        {
            public Task<AuctionDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
