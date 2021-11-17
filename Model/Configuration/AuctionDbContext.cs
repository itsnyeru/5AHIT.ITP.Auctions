using Microsoft.EntityFrameworkCore;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Configuration {
    public class AuctionDbContext : DbContext {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionCategorie> AuctionCategories { get; set; }
        public DbSet<AuctionImage> AuctionImages { get; set; }
        public DbSet<BiddingAuction> BiddingAuctions { get; set; }
        public DbSet<BiddingAuctionBid> BiddingAuctionBids { get; set; }
        public DbSet<BuyAuction> BuyAuction { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //modelBuilder.Configurations.AddFromAssembly(typeof(EntityMap<>).Assembly);
            modelBuilder.Entity<AuctionCategorie>(
                entity => {
                    entity.HasKey(x => new { x.AuctionId, x.Label });
                }
            );

            modelBuilder.Entity<User>(
                entity => {
                    entity.HasIndex(x => x.Username).IsUnique(true);
                    entity.HasIndex(x => x.Email).IsUnique(true);

                    entity.Property(x => x.FirstName).IsRequired(false);
                    entity.Property(x => x.LastName).IsRequired(false);
                    entity.Property(x => x.PhoneNumber).IsRequired(false);
                    entity.Property(x => x.Image).IsRequired(false);

                    entity.HasMany(x => x.Sells).WithOne(y => y.Seller);
                    entity.HasMany(x => x.Buys).WithOne(y => y.Buyer);
                }
            );

            modelBuilder.Entity<Auction>(
                entity => {
                    entity.Property(x => x.FinalPrice).IsRequired(false);
                    entity.Property(x => x.BuyerId).IsRequired(false);
                }
            );

            modelBuilder.Entity<AuctionImage>(
                entity => {
                    
                }
            );

            modelBuilder.Entity<BiddingAuction>(
                entity => {
                    entity.Property(x => x.Step).IsRequired(false);
                    entity.Property(x => x.InstantBuyPrice).IsRequired(false);
                }
            );

            modelBuilder.Entity<BiddingAuctionBid>(
                entity => {
                    entity.HasKey(x => new { x.AuctionId, x.Price });
                }
            );

            modelBuilder.Entity<BuyAuction>(
                entity => {

                }
            );

            modelBuilder.Entity<Categorie>(
                entity => {

                }
            );
        }
    }
}
