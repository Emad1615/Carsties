using AuctionService.Data;
using AuctionService.Features.Auctions.DTOs;
using AuctionService.RequestHelpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Features.Auctions.Queries
{
    public class GetAuctionById
    {
        public class Query : IRequest<Result<AuctionDTO>>
        {
            public int Id { get; set; }
        }
        public class Handler(AuctionDbContext context, IMapper mapper) : IRequestHandler<Query, Result<AuctionDTO>>
        {
            public async Task<Result<AuctionDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var auction = await context.Auctions
                    .Where(a => a.Id == request.Id)
                    .ProjectTo<AuctionDTO>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
                if (auction == null)
                    return Result<AuctionDTO>.Failure("Auction not found", 404);
                return Result<AuctionDTO>.Success(auction);
            }
        }
    }
}
