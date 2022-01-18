
namespace Model.Entity {
    [Table("AUCTIONS_BT")]
    public class Auction {
        [PrimaryKey]
        [AutoIncrement]
        [Column("AUCTION_ID")]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignColumn(ForeignType.MANY_TO_ONE, "SELLER_ID")]
        public User Seller { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Decimal(10,2)]
        public decimal? FinalPrice { get; set; }

        [ForeignColumn(ForeignType.MANY_TO_ONE, "BUYER_ID")]
        public User? Buyer { get; set; }

        public ICollection<AuctionImage> Images { get; set; }

        public ICollection<AuctionCategorie> Categories { get; set; }
    }
}
