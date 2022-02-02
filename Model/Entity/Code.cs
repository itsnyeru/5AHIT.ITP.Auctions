
namespace Model.Entity;

[Table("USER_HAS_CODES", Discriminator = true)]
public class Code {
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignColumn(EForeignType.MANY_TO_ONE, "USER_ID")]
    public User User { get; set; }

    [Nullable]
    public string? Value { get; set; }

    public DateTime ExpiresAt { get; set; }
}

[Table(DiscriminatorValue = "EMAIL_VERIFICATION")]
public class EmailVerificationCode : Code { }

[Table(DiscriminatorValue = "PASSWORD_RESET")]
public class PasswordResetCode : Code { }