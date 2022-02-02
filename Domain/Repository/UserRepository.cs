using EFCAT.Domain.Repository;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository;

public interface IUserRepository : IRepositoryAsync<User> {

}

public class UserRepository : ARepositoryAsync<User>, IUserRepository {
    public UserRepository(AuctionDbContext context) : base(context) { }
}