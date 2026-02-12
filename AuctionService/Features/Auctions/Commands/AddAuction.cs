using AuctionService.Data;
using AuctionService.Entities;
using AuctionService.Features.Auctions.DTOs;
using AuctionService.RequestHelpers;
using AutoMapper;
using Contracts;
using MassTransit;
using MediatR;

namespace AuctionService.Features.Auctions.Commands
{
    public class AddAuction
    {
        public class Command : IRequest<Result<AuctionDTO>>
        {
            public CreateAuctionDTO AuctionDTO { get; set; }
        }
        public class Handler(AuctionDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<Command, Result<AuctionDTO>>
        {
            public async Task<Result<AuctionDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var auction = new Entities.Auction("SYSTEM", request.AuctionDTO.ReservePrice, "SYSTEM", request.AuctionDTO.AuctionEnd);
                auction.AssignItem(mapper.Map<Item>(request.AuctionDTO));
                await context.Auctions.AddAsync(auction, cancellationToken);

                var newAuction = mapper.Map<AuctionDTO>(auction);
                await publishEndpoint.Publish(mapper.Map<AuctionCreated>(newAuction), cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Result<AuctionDTO>.Success(newAuction);
            }
        }
    }
}
