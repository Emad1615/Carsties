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
                // i used transaction here to make sure that if any of the operations fail,
                // the whole process will be rolled back and the database will remain in a consistent state.
                // this is especially important when we are performing multiple operations that depend on each other,
                // such as adding an auction and then publishing an event. if the event publishing fails after the auction has been added,
                // we don't want to end up with an auction in the database that doesn't have a corresponding event published.

                var transaction =await context.Database.BeginTransactionAsync(cancellationToken);
                var auction = new Entities.Auction("SYSTEM", request.AuctionDTO.ReservePrice, "SYSTEM", request.AuctionDTO.AuctionEnd);
                auction.AssignItem(mapper.Map<Item>(request.AuctionDTO));
                await context.Auctions.AddAsync(auction, cancellationToken);
              
                await context.SaveChangesAsync(cancellationToken);

                var newAuction = mapper.Map<AuctionDTO>(auction);
                await publishEndpoint.Publish(mapper.Map<AuctionCreated>(newAuction), cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return Result<AuctionDTO>.Success(newAuction);
            }
        }
    }
}
