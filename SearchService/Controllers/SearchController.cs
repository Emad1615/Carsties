using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearctItems(string searchTerms)
        {
            var query = DB.Default.Find<Item>();
            query.Sort(x => x.Ascending(a => a.Model));
            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                query.Match(Search.Full, searchTerms).SortByTextScore();
            }
            var result = await query.ExecuteAsync();
            return Ok(result);
        }
    }
}
