
namespace Model.Entity {
    [Table("CATEGORIES")]
    public class Categorie {
        [PrimaryKey]
        public string Label {  get; set; }

        public ICollection<AuctionCategorie> Auctions { get; set; }
    }
}
