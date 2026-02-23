namespace AuctionService.Features.Auctions.DTOs
{
    public class AuctionDTO
    {
        public int Slug_Id { get; set; }
        public string ReservePrice { get; set; }
        public string Seller { get; set; }
        public string Winner { get; set; }
        public string Status { get; set; }
        public int? CurrentHighBid { get; private set; }
        public DateTime AuctionEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string ImageUrl { get; set; }
    }
}
