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
        public class Command : IRequest<Result<Unit>>
        {
            public UpdateAuctionDTO AuctionDTO { get; set; }
        }
        public class Handler(AuctionDbContext context, IPublishEndpoint publishEndpoint, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var auction = await context.Auctions.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == request.AuctionDTO.Id, cancellationToken);
                auction.UpdateAuction(request.AuctionDTO.ReservePrice, request.AuctionDTO.AuctionEnd, "SYSTEM");
                auction.UpdateItem(request.AuctionDTO.Make, request.AuctionDTO.Model, request.AuctionDTO.Color, request.AuctionDTO.Mileage, request.AuctionDTO.Year, "SYSTEM");
              
                var updatedAuction = mapper.Map<AuctionDTO>(auction);
                await publishEndpoint.Publish(mapper.Map<AuctionUpdated>(updatedAuction),cancellationToken);
                
                await context.SaveChangesAsync(cancellationToken);
                
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
