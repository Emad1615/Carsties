using AuctionService.Data;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers
{
    public class BidPlacedConsumer(ILogger<BidPlacedConsumer> logger, AuctionDbContext dbContext) : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            logger.LogInformation(" -------->>>> Received BidPlaced event: {@BidPlaced}", context.Message);

            var auction = await dbContext.Auctions.FindAsync(context.Message.AuctionId);
            auction.BidPlaced(context.Message.Amount, context.Message.BidStatus);
            await dbContext.SaveChangesAsync();
        }
    }
}
