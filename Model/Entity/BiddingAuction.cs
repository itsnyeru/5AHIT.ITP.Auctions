using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity {
    [Table("BIDDING_AUCTION")]
    public class BiddingAuction : Auction {
        
        [Required]
        [Column("STARTING_PRICE", TypeName = "decimal(10, 2)")]
        public decimal StartingPrice { get; set; }

       
        [Column("STEP", TypeName = "decimal(10, 2)")]
        public decimal? Step { get; set; }

        
        [Column("INSTANT_BUY_PRICE", TypeName = "decimal(10, 2)")]
        public decimal? InstantBuyPrice { get; set; }

        public ICollection<BiddingAuctionBid> Bidders { get; set; }
    }
}
