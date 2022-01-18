
namespace Model.Entity {
    [Table("BIDDING_AUCTION_BIDS")]
    public class BiddingAuctionBid {
        [PrimaryKey]
        [ForeignColumn(ForeignType.MANY_TO_ONE, "AUCTION_ID")]
        public BiddingAuction Auction { get; set; }

        [Required]
        [Decimal(8, 2)]
        public decimal Price { get; set; }

        [Required]
        public DateTime BidDate { get; set; }

        [Required]
        [ForeignColumn(ForeignType.MANY_TO_ONE, "BIDDER_ID")]
        public User Bidder { get; set; }
    }
}
