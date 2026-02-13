using AuctionService.Data;
using AuctionService.Features.Auctions.DTOs;
using AuctionService.RequestHelpers;
using AutoMapper;
using Contracts;
using MassTransit;
using MediatR;

namespace AuctionService.Features.Auctions.Commands
{
    public class DeleteAuction
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }
        public class Handler(AuctionDbContext context,IPublishEndpoint publishEndpoint,IMapper mapper) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var auction = await context.Auctions.FindAsync(request.Id, cancellationToken);
                auction.SoftDelete("SYSTEM");

                var deletedAuction = mapper.Map<AuctionDTO>(auction);
                await publishEndpoint.Publish(mapper.Map<AuctionDeleted>(deletedAuction), cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
