using MongoDB.Entities;
using SearchService.Models;
using System.Text.Json;

namespace SearchService.Services
{
    public class AuctionServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
    {

        // This method fetches items from the Auction Service that were created after the most recent item in the local database. return json as List<Item>
        public async Task<List<Item>> GetItemForSearchDb()
        {
            string lastCreatedAt = await DB.Default.Find<Item, string>()
                .Sort(x => x.Descending(a => a.CreatedAt))
                .Project(x => x.CreatedAt.ToString())
                .ExecuteFirstAsync();
            var response = await httpClient.GetFromJsonAsync<List<Item>>($"{configuration["AuctionSvcUrl"]}/api/auction?date={lastCreatedAt}");
            return response;
        }

        public async Task<List<Item>> GetItemFRomObjectToSearchDb()
        {

            string lastCreatedAt =await DB.Default.Find<Item, string>()
                .Sort(x => x.Descending(a => a.CreatedAt))
                .Project(x => x.CreatedAt.ToString())
                .ExecuteFirstAsync();
            // If the Auction Service returns a JSON object instead of a JSON array, we need to parse it manually.
            using var stream = await httpClient.GetStreamAsync($"{configuration["AuctionSvcUrl"]}/api/auction?date={lastCreatedAt}");
            // Parse the JSON response and extract the "data" property which contains the list of items.
            using var doc = await JsonDocument.ParseAsync(stream);
            // If the "data" property is not found, return an empty list.
            var oprions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (!doc.RootElement.TryGetProperty("data", out var jsonElement))
            {
                return JsonSerializer.Deserialize<List<Item>>(jsonElement, oprions);
            }
            var items = JsonSerializer.Deserialize<List<Item>>(jsonElement.GetRawText(), oprions);
            return items;
        }
    }
}
