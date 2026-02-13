using AutoMapper;
using Contracts;
using MassTransit;

namespace SearchService.Consumers
{
    public class AuctionUpdatedConsumer(IMapper mapper) : IConsumer<AuctionUpdated>
    {
        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
            Console.WriteLine($"Received AuctionUpdated event for Slug_Id: {context.Message.Slug_Id}, Make: {context.Message.Make}, Model: {context.Message.Model}");


            await Task.CompletedTask;
        }
    }
}
