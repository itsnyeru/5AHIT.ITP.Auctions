using EFCAT.Model.Data.Annotation;

namespace Model.Entity {
    [Table("USERS")]
    public class User {
        [PrimaryKey]
        [AutoIncrement]
        [Column("USER_ID")]
        public int Id { get; set; }

        [Unique]
        [Required]
        [Varchar(16, Min = 3)]
        public string Username { get; set; }

        [Unique]
        [Required]
        [EmailAddress]
        [Varchar(255)]
        public string Email { get; set; }

        [Required]
        public Crypt<SHA256> Password { get; set; }

        [Nullable]
        [Varchar(32)]
        public string? FirstName { get; set; }

        [Nullable]
        [Varchar(32)]
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [Number(8, 2)]
        public decimal Balance { get; set; } = 0;

        public Image? Image { get; set; }

        public bool IsAdmin { get; set; } = false;

        [ReferenceColumn("Seller")]
        public ICollection<Auction> Sells { get; set; }

        [ReferenceColumn("Buyer")]
        public ICollection<Auction> Buys { get; set; }

        [ReferenceColumn("Bidder")]
        public ICollection<BiddingAuctionBid> Bids { get; set; }

        public ICollection<Code> Codes { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
