namespace AuctionService.Features.Auctions.DTOs
{
    public class CreateAuctionDTO : BaseAuctionDTO
    {
        public string Seller { get; set; }
        public string CreatedBy { get; set; }
    }
}
