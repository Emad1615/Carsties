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
            public string Date { get; set; }
        }
        public class Handler(AuctionDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<AuctionDTO>>>
        {
            public async Task<Result<List<AuctionDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {

                var query = context.Auctions.Where(x => !x.IsDeleted).OrderBy(x => x.Item.Make).AsQueryable();

                if (!string.IsNullOrEmpty(request.Date))
                {
                    var date = DateTime.Parse(request.Date).ToUniversalTime();
                    query = query.Where(x => x.CreatedAt.CompareTo(date) > 0);
                }

                var auctions = await query.AsNoTracking()
                                          .ProjectTo<AuctionDTO>(mapper.ConfigurationProvider)
                                          .ToListAsync();

                return Result<List<AuctionDTO>>.Success(auctions);
            }
        }
    }
}
