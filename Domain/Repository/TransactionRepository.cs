using EFCAT.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository;

public interface ITransactionRepository : IRepositoryAsync<Transaction> {
    Task Approve(int userId, Guid token);
}

public class TransactionRepository : ARepositoryAsync<Transaction>, ITransactionRepository {
    public TransactionRepository(AuctionDbContext context) : base(context) { }

    public async Task Approve(int userId, Guid token) {
        Transaction? transaction = await _set.Include(o => o.User).FirstOrDefaultAsync(o => o.User.Id == userId && o.Token == token);
        if (transaction == null || DateTime.Now > transaction.ValidUntil || transaction.IsApproved) return;
        transaction.IsApproved = true;
        transaction.User.Balance += transaction.Balance;
        await UpdateAsync(transaction);
    }
}
