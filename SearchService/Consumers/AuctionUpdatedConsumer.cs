using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionUpdatedConsumer(IMapper mapper) : IConsumer<AuctionUpdated>
    {
        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
            Console.WriteLine($"Received AuctionUpdated event for Autction with ID : {context.Message.Slug_Id}, Make: {context.Message.Make}, Model: {context.Message.Model}");

            var item = mapper.Map<Item>(context.Message);

            var db = await DB.InitAsync("SearchBb");
            await db.Update<Item>().Match(x => x.Slug_Id == item.Slug_Id)
                  .Modify(x => x.Set(y => y.Color, item.Color))
                  .Modify(x => x.Set(y => y.Make, item.Make))
                  .Modify(x => x.Set(y => y.Model, item.Model))
                  .Modify(x => x.Set(y => y.Year, item.Year))
                  .Modify(x => x.Set(y => y.Mileage, item.Mileage))
                  .ExecuteAsync();

            await Task.CompletedTask;
        }
    }
}
