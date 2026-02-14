using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionDeletedConsumer(IMapper mapper) : IConsumer<AuctionDeleted>
    {
        public async Task Consume(ConsumeContext<AuctionDeleted> context)
        {
            Console.WriteLine($"Received AuctionDeleted event for auction with ID: {context.Message.Slug_Id}");

            var item = mapper.Map<AuctionDeleted>(context.Message);

            var db = await DB.InitAsync("SearchBb");
            var auction = await db.Find<Item>().Match(x => x.Slug_Id == item.Slug_Id).ExecuteFirstAsync();
            await db.DeleteAsync(auction);

            await Task.CompletedTask;
        }
    }
}
