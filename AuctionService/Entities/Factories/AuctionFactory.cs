namespace AuctionService.Entities.Factories
{
    public static class AuctionFactory
    {
        public static Auction Create(string createdBy, int reservePrice, string seller, DateTime auctionEnd, Item item)
        {
            var auction = new Auction("SYSTEM", reservePrice, seller, auctionEnd);
            auction.AssignItem(item);
            return auction;
        }
    }
}
