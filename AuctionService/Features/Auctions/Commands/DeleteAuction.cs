using AuctionService.Data;
using AuctionService.RequestHelpers;
using MediatR;

namespace AuctionService.Features.Auctions.Commands
{
    public class DeleteAuction
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }
        public class Handler(AuctionDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var auction = await context.Auctions.FindAsync(request.Id, cancellationToken);
                auction.SoftDelete("SYSTEM");
                await context.SaveChangesAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
