using EFCAT.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository;

public interface IUserRepository : IRepositoryAsync<User> {
    Task<User?> GetUserByIdentity(string identity);
}

public class UserRepository : ARepositoryAsync<User>, IUserRepository {
    public UserRepository(AuctionDbContext context) : base(context) { }

    public async Task<User?> GetUserByIdentity(string identity) => await _set.FirstOrDefaultAsync(u => u.Username.ToUpper() == identity.ToUpper() || u.Email.ToUpper() == identity.ToUpper());
}