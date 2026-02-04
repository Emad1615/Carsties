namespace AuctionService.Entities
{
    public class Auction : BaseEntity
    {
        public int ReservePrice { get; private set; } = 0;
        public string Seller { get; private set; } = string.Empty;
        public string Winner { get; private set; } = string.Empty;
        public int SoldAmount { get; private set; }
        public int CurrentHighBid { get; private set; }
        public Status Status { get; private set; }
        public DateTime AuctionEnd { get; private set; }
        private Item _item;
        public Item Item => _item;
        private Auction() : base("SYSTEM") { }
        public Auction(string createdBy, int reservePrice, string seller, DateTime auctionEnd) : base(createdBy)
        {
            ReservePrice = reservePrice;
            Seller = seller ?? string.Empty;
            AuctionEnd = auctionEnd;
            Status = Status.Live;
        }

        public void AssignItem(Item item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (_item is not null)
                throw new InvalidOperationException("Auction already has an item");
            _item = item;
        }

        public void UpdateAuction(int reservePrice, DateTime auctionEnd,string modifiedBy)
        {
            ReservePrice = reservePrice;
            AuctionEnd = auctionEnd;
            SetModified(modifiedBy);
        }
        public void UpdateItem(string make, string model, string color, int mileage, int year, string modifiedBy)
        {
            if (_item is null)
                throw new ArgumentNullException("No Item Assigned to auction");
            _item.Update(make, model, color, mileage, year, modifiedBy);
        }

    }
}
