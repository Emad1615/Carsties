using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionCreatedConsumer(IMapper mapper) : IConsumer<AuctionCreated>
    {
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            Console.WriteLine($"Received AuctionCreated event for Slug_Id: {context.Message.Slug_Id}");
            var item = mapper.Map<Item>(context.Message);
            await DB.Default.SaveAsync(item);
        }
    }
}
