using System;

namespace Model.Entity {
    [Table("BIDDING_AUCTION")]
    public class BiddingAuction : Auction {
        [Required]
        [Number(8, 2)]
        public decimal StartingPrice { get; set; }

       
        [Number(10, 2)]
        public decimal? Step { get; set; }

        
        [Number(10, 2)]
        public decimal? InstantBuyPrice { get; set; }

        [ReferenceColumn("Auction")]
        public ICollection<BiddingAuctionBid> Bidders { get; set; }
    }
}
