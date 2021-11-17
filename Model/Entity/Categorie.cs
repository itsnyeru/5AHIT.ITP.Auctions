using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity {
    [Table("CATEGORIES")]
    public class Categorie {
        [Key]
        [Column("LABEL")]
        public string Label {  get; set; }

        public ICollection<AuctionCategorie> Auctions { get; set; }
    }
}
