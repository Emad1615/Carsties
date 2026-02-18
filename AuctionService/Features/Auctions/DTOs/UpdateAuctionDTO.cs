namespace AuctionService.Features.Auctions.DTOs
{
    public class UpdateAuctionDTO:BaseAuctionDTO
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
}
