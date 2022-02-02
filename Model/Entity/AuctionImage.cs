namespace Model.Entity {
    [Table("AUCTION_IMAGES")]
    public class AuctionImage {
        [PrimaryKey]
        [AutoIncrement]
        [Column("IMAGE_ID")]
        public int ImageId { get; set; }

        [ForeignColumn(EForeignType.MANY_TO_ONE, "AUCTION_ID")]
        public Auction Auction { get; set; }

        public Image Image { get; set; }
    }
}
