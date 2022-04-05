
namespace Model.Entity;

[Table("TRANSACTIONS")]
public class Transaction {
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    [ForeignColumn(EForeignType.MANY_TO_ONE, "USER_ID")]
    public User User { get; set; }

    [NotNull]
    public Guid Token { get; set; } = Guid.NewGuid();

    [NotNull]
    [Decimal(16,2)]
    public decimal Balance { get; set; }

    [Enum(typeof(string))]
    public ETransactionType Type { get; set; } = ETransactionType.CHARGE;

    [NotNull]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [NotNull]
    public DateTime ValidUntil { get; set; } = DateTime.Now.AddMinutes(10);

    [NotNull]
    public bool IsApproved { get; set; } = false;
}

public enum ETransactionType {
    CHARGE, WITHDRAW
}