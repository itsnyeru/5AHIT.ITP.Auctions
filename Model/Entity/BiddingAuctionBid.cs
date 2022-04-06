
namespace Model.Entity {
    [Table("BIDDING_AUCTION_BIDS")]
    public class BiddingAuctionBid {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [ForeignColumn(EForeignType.MANY_TO_ONE, "AUCTION_ID")]
        public BiddingAuction Auction { get; set; }

        [Required]
        [Number(8, 2)]
        public decimal Price { get; set; }

        [Required]
        public DateTime BidDate { get; set; }

        [Required]
        [ForeignColumn(EForeignType.MANY_TO_ONE, "BIDDER_ID")]
        public User Bidder { get; set; }
    }
}
