
namespace Model.Entity {
    [Table("AUCTION_HAS_CATEGORIES")]
    public class AuctionCategorie {
        [PrimaryKey]
        [ForeignColumn(EForeignType.MANY_TO_ONE, "CATEGORIE")]
        public Categorie Categorie { get; set; }

        [PrimaryKey]
        [ForeignColumn(EForeignType.MANY_TO_ONE, "AUCTION_ID")]
        public Auction Auction { get; set; }
    }
}
