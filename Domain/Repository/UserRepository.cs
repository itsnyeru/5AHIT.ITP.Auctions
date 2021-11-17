using EFCAT.Repository;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository {
    public interface IUserRepository : IRepositoryAsync<User, int> { }

    public class UserRepository : ARepositoryAsync<User, int>, IUserRepository {
        public UserRepository(AuctionDbContext context) : base(context) {  }
    }
}
