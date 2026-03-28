using AuctionService.Data;
using AuctionService.Features.Auctions.DTOs;
using AuctionService.RequestHelpers;
using AutoMapper;
using Contracts;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Features.Auctions.Commands
{
    public class UpdateAuction
    {
        public class Command : IRequest<Result<int>>
        {
            public UpdateAuctionDTO AuctionDTO { get; set; }
        }
        public class Handler(AuctionDbContext context, IPublishEndpoint publishEndpoint, IMapper mapper) : IRequestHandler<Command, Result<int>>
        {
            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                var auction = await context.Auctions.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == request.AuctionDTO.Id, cancellationToken);
                if (auction is null) return Result<int>.Failure("No Auction not found.", 404);
                if (auction.CreatedBy != request.AuctionDTO.ModifiedBy)
                    return Result<int>.Failure("Access denied. only the auction owner can edit this resource", 403);
                auction.UpdateAuction(request.AuctionDTO.ReservePrice, request.AuctionDTO.AuctionEnd, request.AuctionDTO.ModifiedBy);
                auction.UpdateItem(request.AuctionDTO.Make, request.AuctionDTO.Model, request.AuctionDTO.Color, request.AuctionDTO.Mileage, request.AuctionDTO.Year, request.AuctionDTO.ModifiedBy);

                var updatedAuction = mapper.Map<AuctionDTO>(auction);
                await publishEndpoint.Publish(mapper.Map<AuctionUpdated>(updatedAuction), cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Result<int>.Success(updatedAuction.AuctionId);
            }
        }
    }
}
