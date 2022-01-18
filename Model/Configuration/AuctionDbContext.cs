using EFCAT.Model.Configuration;
using Microsoft.EntityFrameworkCore;
using Model.Entity;

namespace Model.Configuration {
    public class AuctionDbContext : DatabaseContext {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionCategorie> AuctionCategories { get; set; }
        public DbSet<AuctionImage> AuctionImages { get; set; }
        public DbSet<BiddingAuction> BiddingAuctions { get; set; }
        public DbSet<BiddingAuctionBid> BiddingAuctionBids { get; set; }
        public DbSet<BuyAuction> BuyAuction { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

    }
}
