using AuctionService.Data;
using AuctionService.Entities;
using AuctionService.Features.Auctions.DTOs;
using AutoMapper;
using MediatR;

namespace AuctionService.Features.Auctions.Commands
{
    public class AddAuction
    {
        public class Command : IRequest<AuctionDTO>
        {
            public CreateAuctionDTO AuctionDTO { get; set; }
        }
        public class Handler(AuctionDbContext context, IMapper mapper) : IRequestHandler<Command, AuctionDTO>
        {
            public async Task<AuctionDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var auction = new Entities.Auction("SYSTEM", request.AuctionDTO.ReservePrice, "SYSTEM", request.AuctionDTO.AuctionEnd);
                auction.AssignItem(mapper.Map<Item>(request.AuctionDTO));
                await context.Auctions.AddAsync(auction, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return mapper.Map<AuctionDTO>(auction);
            }
        }
    }
}
