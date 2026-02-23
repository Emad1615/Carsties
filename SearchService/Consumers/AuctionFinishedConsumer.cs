using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionFinishedConsumer(ILogger<AuctionFinishedConsumer> logger) : IConsumer<AuctionFinished>
    {
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            logger.LogInformation(" -------->>>> Received AuctionFinished event for AuctionId: {AuctionId}, ItemSold: {ItemSold}, Seller: {Seller}, Winner: {Winner}, Amount: {Amount}",
                context.Message.AuctionId, context.Message.ItemSold, context.Message.Seller, context.Message.Winner, context.Message.Amount);
            var db = await DB.InitAsync("SearchBb");
            if (context.Message.ItemSold)
            {
                await db.Update<Item>()
                     .Match(i => i.Slug_Id == context.Message.AuctionId)
                     .Modify(x => x.Winner, context.Message.Winner)
                     .Modify(x => x.SoldAmount, (int)context.Message.Amount)
                     .Modify(x => x.Status, "Finished")
                     .ExecuteAsync();
            }
            await db.Update<Item>()
                     .Match(i => i.Slug_Id == context.Message.AuctionId)
                     .Modify(x => x.Status, "ReserveNotMet")
                     .ExecuteAsync();
        }
    }
}
