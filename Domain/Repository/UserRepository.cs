using EFCAT.Domain.Repository;
using EFCAT.Model.Data;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository;

public interface IUserRepository : IRepositoryAsync<User> {
    Task<User?> GetUserByIdentity(string identity);
    Task<User?> GetUserById(int userId);
    Task<Crypt<SHA256>> SetPasswordByUser(int userId, string password);
    Task<decimal> GetBalance(int userId);

    Task<List<Auction>> GetCreatedAuctionsByUser(int userId);
    Task<List<BiddingAuction>> GetParticipatingAuctionsByUser(int userId);
    Task<List<Auction>> GetWonAuctionsByUser(int userId);
}

public class UserRepository : ARepositoryAsync<User>, IUserRepository {
    public UserRepository(AuctionDbContext context) : base(context) { }

    public async Task<User?> GetUserByIdentity(string identity) => await _set.FirstOrDefaultAsync(u => u.Username.ToUpper() == identity.ToUpper() || u.Email.ToUpper() == identity.ToUpper());
    public async Task<User?> GetUserById(int userId) => await _set.FirstOrDefaultAsync(o => o.Id == userId);
    public async Task<Crypt<SHA256>> SetPasswordByUser(int userId, string password) {
        User? user = await GetUserById(userId);
        user.Password = password;
        await UpdateAsync(user);
        return user.Password;
    }
    public async Task<decimal> GetBalance(int userId) => (await _set.Select(o => new { Id = o.Id, Balance = o.Balance}).FirstOrDefaultAsync(o => o.Id == userId)).Balance;
    public async Task<List<Auction>> GetCreatedAuctionsByUser(int userId) => await _context.Set<Auction>().Include(o => o.Seller).Where(o => o.Seller.Id == userId).ToListAsync();
    public async Task<List<BiddingAuction>> GetParticipatingAuctionsByUser(int userId) => await _set.Include(o => o.Bids).ThenInclude(o => o.Auction).Where(o => o.Id == userId).SelectMany(o => o.Bids.Select(o => o.Auction)).ToListAsync();
    public async Task<List<Auction>> GetWonAuctionsByUser(int userId) => await _context.Set<Auction>().Include(o => o.Buyer).Where(o => o.Buyer.Id == userId).ToListAsync();
}