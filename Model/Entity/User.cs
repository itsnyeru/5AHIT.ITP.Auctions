namespace Model.Entity {
    [Table("USERS")]
    public class User {
        [PrimaryKey]
        [AutoIncrement]
        [Column("USER_ID")]
        public int Id { get; set; }

        [Required]
        [Varchar(16, Min = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Varchar(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public Crypt<SHA256> Password { get; set; }

        [Varchar(32)]
        public string? FirstName { get; set; }

        [Varchar(32)]
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [Decimal(8, 2)]
        public decimal Balance { get; set; } = 0;

        public Image? Image { get; set; }

        [Bool]
        public bool IsAdmin { get; set; } = false;

        [ReferenceColumn("Seller")]
        public ICollection<Auction> Sells { get; set; }

        [ReferenceColumn("Buyer")]
        public ICollection<Auction> Buys { get; set; }
    }
}
