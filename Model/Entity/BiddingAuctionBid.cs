using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity {
    [Table("BIDDING_AUCTION_BIDS")]
    public class BiddingAuctionBid {

        [Column("AUCTION_ID")]
        public int AuctionId { get; set; }
        [ForeignKey("AuctionId")]
        public BiddingAuction Auction { get; set; }

        [Required]
        [Column("PRICE", TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        [Column("BID_DATE")]
        public DateTime BidDate { get; set; }

        [Required]
        [Column("BIDDER_ID")]
        public int BidderId { get; set; }
        [ForeignKey("BidderId")]
        public User Bidder { get; set; }
    }
}
