namespace AuctionService.Features.Auctions.DTOs
{
    public class BaseAuctionDTO
    {
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
    }
}
