using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;
using System.Text.Json;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task DbInit(WebApplication app)
        {
            await DB.InitAsync("SearchBb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Default.Index<Item>()
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .Key(x => x.Year, KeyType.Descending)
                .CreateAsync();

            using var scope= app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var auctionService=service.GetRequiredService<AuctionServiceHttpClient>();
            var auctions = await auctionService.GetItemFRomObjectToSearchDb();
            if (auctions.Count() > 0)
            {
                Console.WriteLine("Updating search database with new auction items...");
                await DB.Default.SaveAsync(auctions);
            }

            #region Old seeding logic from json file, now replaced by fetching data from Auction Service
            //var count = await DB.Default.CountAsync<Item>();
            //if (count == 0)
            //{
            //    Console.WriteLine("Seeding initial data into the database...");
            //    var itemData = await File.ReadAllTextAsync("Data/auctions.json");
            //    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            //    var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
            //    await DB.Default.SaveAsync(items);
            //}
            #endregion
        }
    }
}
