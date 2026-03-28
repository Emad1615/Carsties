using AuctionService.Entities;
using AuctionService.Entities.Factories;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    public class DbInitializer
    {
        public static void DbInit(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            SeedData(service.GetRequiredService<AuctionDbContext>());
        }
        public static void SeedData(AuctionDbContext context)
        {
            context.Database.Migrate();
            if (!context.Auctions.Any())
            {
                var auctions = new List<Auction>
                            {
                            // 1 Ford GT
                            AuctionFactory.Create(
                                "emad",
                                20000,
                                "emad",
                                DateTime.UtcNow.AddDays(10),
                                new Item("emad", "Ford", "GT", "White", 50000, 2020,
                                    "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg")
                            ),

                            // 2 Bugatti Veyron
                            AuctionFactory.Create(
                                "emad",
                                90000,
                                "emad",
                                DateTime.UtcNow.AddDays(60),
                                new Item("emad", "Bugatti", "Veyron", "Black", 15035, 2018,
                                    "https://cdn.pixabay.com/photo/2012/05/29/00/43/car-49278_960_720.jpg")
                            ),

                            // 3 Ford Mustang
                            AuctionFactory.Create(
                                "emad",
                                0,
                                "emad",
                                DateTime.UtcNow.AddDays(4),
                                new Item("emad", "Ford", "Mustang", "Black", 65125, 2023,
                                    "https://cdn.pixabay.com/photo/2012/11/02/13/02/car-63930_960_720.jpg")
                            ),


                            // 4 BMW X1
                            AuctionFactory.Create(
                                "emad",
                                20000,
                                "emad",
                                DateTime.UtcNow.AddDays(30),
                                new Item("emad", "BMW", "X1", "White", 90000, 2017,
                                    "https://cdn.pixabay.com/photo/2017/08/31/05/47/bmw-2699538_960_720.jpg")
                            ),

                            // 5 Ferrari Spider
                            AuctionFactory.Create(
                                "emad",
                                20000,
                                "emad",
                                DateTime.UtcNow.AddDays(45),
                                new Item("emad", "Ferrari", "Spider", "Red", 50000, 2015,
                                    "https://cdn.pixabay.com/photo/2017/11/09/01/49/ferrari-458-spider-2932191_960_720.jpg")
                            ),

                            // 6 Ferrari F-430
                            AuctionFactory.Create(
                                "emad",
                                150000,
                                "emad",
                                DateTime.UtcNow.AddDays(13),
                                new Item("emad", "Ferrari", "F-430", "Red", 5000, 2022,
                                    "https://cdn.pixabay.com/photo/2017/11/08/14/39/ferrari-f430-2930661_960_720.jpg")
                            ),

                            // 7 Audi R8
                            AuctionFactory.Create(
                                "emad",
                                0,
                                "emad",
                                DateTime.UtcNow.AddDays(19),
                                new Item("emad", "Audi", "R8", "White", 10050, 2021,
                                    "https://cdn.pixabay.com/photo/2019/12/26/20/50/audi-r8-4721217_960_720.jpg")
                            ),

                            // 8 Audi TT
                            AuctionFactory.Create(
                                "emad",
                                20000,
                                "emad",
                                DateTime.UtcNow.AddDays(20),
                                new Item("emad", "Audi", "TT", "Black", 25400, 2020,
                                    "https://cdn.pixabay.com/photo/2016/09/01/15/06/audi-1636320_960_720.jpg")
                            ),

                            // 9 Ford Model T
                            AuctionFactory.Create(
                                "emad",
                                20000,
                                "emad",
                                DateTime.UtcNow.AddDays(48),
                                new Item("emad", "Ford", "Model T", "Rust", 150150, 1938,
                                    "https://cdn.pixabay.com/photo/2017/08/02/19/47/vintage-2573090_960_720.jpg")
                            )
                        };

                context.Auctions.AddRange(auctions);
                context.SaveChanges();
            }
        }
    }
}
