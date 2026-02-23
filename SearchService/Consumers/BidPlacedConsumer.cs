using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class BidPlacedConsumer(ILogger<BidPlacedConsumer> logger) : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            logger.LogInformation(" -------->>>> Received BidPlaced event for AuctionId: {AuctionId}, Bidder: {Bidder}, Amount: {Amount}",
                    context.Message.AuctionId, context.Message.Bidder, context.Message.Amount);

            var db = await DB.InitAsync("SearchBb");
            var auction =await db.Find<Item>().Match(x => x.Slug_Id == context.Message.AuctionId).ExecuteFirstAsync();
            if(context.Message.Amount > auction.SoldAmount && context.Message.BidStatus.Contains("Accepted"))
            {
               await db.Update<Item>()
                    .Modify(x => x.CurrentHighBid,context.Message.Amount).ExecuteAsync();
            }
        }
    }
}
