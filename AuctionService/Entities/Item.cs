using System.Drawing;

namespace AuctionService.Entities
{
    public class Item : BaseEntity
    {
        public string Color { get; private set; }
        public string Make { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public int Mileage { get; private set; }
        public string ImageUrl { get; private set; }
        public int AuctionId { get; private set; }
        public Auction Auction { get; private set; }
        private Item() : base("SYSTEM") { }
        public Item(string createdBy, string make, string model, string color, int mileage, int year, string imageUrl) : base(createdBy)
        {
            Make = make;
            Model = model;
            Color = color;
            Mileage = mileage;
            Year = year;
            ImageUrl = imageUrl;
        }
        public void Update(string make, string model, string color, int mileage, int year, string modifiedBy)
        {
            Make = make;
            Model = model;
            Color = color;
            Mileage = mileage;
            Year = year;
            SetModified(modifiedBy);
        }
    }
}
