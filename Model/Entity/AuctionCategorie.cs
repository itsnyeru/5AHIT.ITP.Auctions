using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity {
    [Table("AUCTION_HAS_CATEGORIES")]
    public class AuctionCategorie {
        [Column("CATEGORIE")]
        public string Label { get; set; }
        [ForeignKey("Label")]
        public Categorie Categorie { get; set; }


        [Column("AUCTION_ID")]
        public int AuctionId { get; set; }
        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; }
    }
}
