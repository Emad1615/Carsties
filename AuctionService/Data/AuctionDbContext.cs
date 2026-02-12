using AuctionService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Auction>()
                .HasOne(x=>x.Item)
                .WithOne(x=>x.Auction)
                .HasForeignKey<Item>(x=>x.AuctionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
        }
        public required DbSet<Auction> Auctions { get; set; }
        public required DbSet<Item> Items { get; set; }
    }
}
