using AuctionService.Data;
using AuctionService.Features.Auctions.DTOs;
using AuctionService.RequestHelpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Features.Auctions.Queries
{
    public class GetAllAuctions
    {
        public class Query : IRequest<Result<List<AuctionDTO>>>
        {
        }
        public class Handler(AuctionDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<AuctionDTO>>>
        {
            public async Task<Result<List<AuctionDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var auctions = await context.Auctions.Where(x=>!x.IsDeleted)
                    .AsNoTracking()
                    .ProjectTo<AuctionDTO>(mapper.ConfigurationProvider)
                    .ToListAsync();
                return Result<List<AuctionDTO>>.Success(auctions);
            }
        }
    }
}
