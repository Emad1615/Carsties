namespace AuctionService.Features.Auction.DTOs
{
    public class CreateAuctionDTO : BaseAuctionDTO
    {
        public int ReservePrice { get; set; }
        public DateTime AuctionEnd { get; set; }
        public string ImageUrl { get; set; }
    }
}
