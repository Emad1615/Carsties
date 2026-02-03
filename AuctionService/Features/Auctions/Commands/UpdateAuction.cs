using AuctionService.Data;
using AuctionService.Features.Auctions.DTOs;
using AuctionService.RequestHelpers;
using MediatR;

namespace AuctionService.Features.Auctions.Commands
{
    public class UpdateAuction
    {
        public class Command : IRequest<Result<Unit>>
        {
            public UpdateAuctionDTO AuctionDTO { get; set; }
        }
        public class Handler(AuctionDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var auction = await context.Auctions.FindAsync(request.AuctionDTO.Id, cancellationToken);
                auction.UpdateItem(request.AuctionDTO.Make, request.AuctionDTO.Model, request.AuctionDTO.Color, request.AuctionDTO.Mileage, request.AuctionDTO.Year, "SYSTEM");
                await context.SaveChangesAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
