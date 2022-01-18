using EFCAT.Repository;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository {
    public interface IUserRepository : IRepositoryAsync<User, int> {
        Task<User?> GetAccount(int id);
        Task<User?> GetAccount(string identifier, string password);
    }

    public class UserRepository : ARepositoryAsync<User, int>, IUserRepository {
        public UserRepository(AuctionDbContext context) : base(context) {  }

        public async Task<User?> GetAccount(int id) => await _entitySet.FindAsync(id);

        /* Returns a User after  */
        public async Task<User?> GetAccount(string identifier, string password) {
            var user = _entitySet.SingleOrDefault(u => u.Email == identifier || u.Username == identifier);
            if (user == null) return await Task.FromResult(user);
            return await Task.FromResult(user.Password.Verify(password) ? user : null);
        }
    }
}
