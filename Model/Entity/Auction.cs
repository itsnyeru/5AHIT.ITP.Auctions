
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
        [ForeignColumn(EForeignType.MANY_TO_ONE, "SELLER_ID")]
        public User Seller { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Number(10,2)]
        public decimal? FinalPrice { get; set; }

        [NotNull]
        [Enum(typeof(string))]
        public EDeliveryType DeliveryType { get; set; } = EDeliveryType.DELIVER;

        [ForeignColumn(EForeignType.MANY_TO_ONE, "BUYER_ID")]
        public User? Buyer { get; set; }

        [ReferenceColumn("Auction")]
        public ICollection<AuctionImage> Images { get; set; }

        [ReferenceColumn("Auction")]
        public ICollection<AuctionCategorie> Categories { get; set; }
    }
}

public enum EDeliveryType {
    DELIVER, PICK_UP
}