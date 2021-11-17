using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity {
    [Table("AUCTIONS")]
    public class Auction {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AUCTION_ID")]
        public int Id { get; set; }

        [Required]
        [Column("TITLE")]
        public string Title { get; set; }

        [Required]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Required]
        [Column("SELLER_ID")]
        public int SellerId { get; set; }
        [ForeignKey("SellerId")]
        public User Seller { get; set; }

        [Required]
        [Column("START_DATE")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("END_DATE")]
        public DateTime EndDate { get; set; }

        [Column("FINAL_PRICE", TypeName = "decimal(10,2)")]
        public decimal? FinalPrice { get; set; }

        [Column("BUYER_ID")]
        public int? BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public User Buyer { get; set; }

        public ICollection<AuctionImage> Images { get; set; }

        public ICollection<AuctionCategorie> Categories { get; set; }
    }
}
