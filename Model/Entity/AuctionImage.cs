using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity {
    [Table("AUCTION_IMAGES")]
    public class AuctionImage {
        [Key]
        [Column("IMAGE_ID")]
        public int ImageId { get; set; }

        [Column("AUCTION_ID")]
        public int AuctionId { get; set; }
        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; }

        [MaxLength]
        [Column("IMAGE")]
        public byte[] Image { get; set; }
    }
}
