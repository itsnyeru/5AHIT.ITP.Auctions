using System;

namespace Model.Entity {
    [Table("BIDDING_AUCTION")]
    public class BiddingAuction : Auction {
        
        [Required]
        [Decimal(8, 2)]
        public decimal StartingPrice { get; set; }

       
        [Decimal(10, 2)]
        public decimal? Step { get; set; }

        
        [Decimal(10, 2)]
        public decimal? InstantBuyPrice { get; set; }

        public ICollection<BiddingAuctionBid> Bidders { get; set; }
    }
}
