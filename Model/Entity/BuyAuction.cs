using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity {
    [Table("BUY_AUCTION")]
    public class BuyAuction : Auction {
        [Required]
        [Column("MINIMUM_PRICE", TypeName = "decimal(10, 2)")]
        public decimal MinimumPrice { get; set; }
    }
}
