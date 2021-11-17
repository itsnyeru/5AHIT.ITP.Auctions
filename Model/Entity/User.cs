using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entity {
    [Table("USERS")]
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("USER_ID")]
        public int Id { get; set; }

        [Required]
        [Column("USERNAME")]
        [MinLength(3)]
        [MaxLength(16)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Column("EMAIL")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [Column("PASSWORD")]
        [MaxLength(256)]
        public string Password { get; set; }

        [Column("FIRST_NAME")]
        [MaxLength(32)]
        public string? FirstName { get; set; }

        [Column("LAST_NAME")]
        [MaxLength(32)]
        public string? LastName { get; set; }

        [Phone]
        [Column("PHONE_NUMBER")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Column("BALANCE", TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; } = 0;

        [MaxLength]
        [Column("IMAGE")]
        public byte[]? Image { get; set; }

        public ICollection<Auction> Sells { get; set; }

        public ICollection<Auction> Buys { get; set; }
    }
}
