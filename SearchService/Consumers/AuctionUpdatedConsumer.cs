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
                .ModifyOnly(x => new { x.Color, x.Make, x.Model, x.Year, x.Mileage, x.ReservePrice, x.AuctionEnd }, item).ExecuteAsync();
        }
    }
}
