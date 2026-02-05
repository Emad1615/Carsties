using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;
namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearctItems([FromQuery] SearchParams searchParams)
        {
            var query = DB.Default.PagedSearch<Item, Item>();

            // Full-Text Search
            if (!string.IsNullOrWhiteSpace(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }
            // Filtering by Seller
            if (!string.IsNullOrWhiteSpace(searchParams.Seller))
            {
                query.Match(x => x.Seller == searchParams.Seller);
            }
            // Filtering by Winner
            if (!string.IsNullOrWhiteSpace(searchParams.Winner))
            {
                query.Match(x => x.Winner == searchParams.Winner);
            }

            // Ordering
            query = searchParams.OrderBy switch
            {
                "make" => query.Sort(x => x.Ascending(a => a.Make)),
                "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
                _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
            };

            // Additional Filtering
            query = searchParams.FilterBy switch
            {
                "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
                "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow),
                _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow)
            };

            query.PageNumber(searchParams.PageNumber);
            query.PageSize(searchParams.PageSize);

            var result = await query.ExecuteAsync();
            return Ok(new
            {
                result = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            });
        }
    }
}
