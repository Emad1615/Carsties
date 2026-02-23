using AuctionService.Data;
using AuctionService.Entities;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers
{
    public class AuctionFinishedConsumer(ILogger<AuctionFinishedConsumer> logger, AuctionDbContext dbContext) : IConsumer<AuctionFinished>
    {
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            logger.LogInformation(" -------->>>> Received AuctionFinished event for AuctionId: {AuctionId}", context.Message.AuctionId);


            var auction = await dbContext.Auctions.FindAsync(context.Message.AuctionId);

            auction.FinishAuction(context.Message.Amount.Value, context.Message.Winner, context.Message.ItemSold);

            await dbContext.SaveChangesAsync();

        }
    }
}
