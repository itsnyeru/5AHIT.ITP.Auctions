using EFCAT.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository;

public interface IAuctionRepository : IRepositoryAsync<Auction> {
    Task<Auction> Get(int auctionId);
    Task<List<Auction>> All(Auction? lastElement = null);
    Task<List<Auction>> Filter(string title, List<Categorie> categories, Auction? lastElement = null);
    Task<List<Categorie>> GetCategories();
    Task<List<Auction>> GetSellerProducts(User user);
    Task<List<Auction>> GetSimiliarProducts(Auction auction);
    Task<List<Auction>> GetPanelAuctions(string name);
    Task<List<BiddingAuctionBid>> GetBids(int auctionId);
    Task<bool> Bet(BiddingAuctionBid bid);
    Task<bool> Buy(int auctionId, int userId, decimal price);
}

public class AuctionRepository : ARepositoryAsync<Auction>, IAuctionRepository {
    public AuctionRepository(AuctionDbContext context) : base(context) { }

    public async Task<Auction> Get(int auctionId) {
        if (await _context.Set<BiddingAuction>().AnyAsync(o => o.Id == auctionId)) return await _context.Set<BiddingAuction>().Include(o => o.Images).ThenInclude(o => o.Image).Include(o => o.Seller).Include(o => o.Bidders).ThenInclude(o => o.Bidder).Include(o => o.Categories).ThenInclude(o => o.Categorie).FirstOrDefaultAsync(o => o.Id == auctionId);
        else return await _context.Set<BuyAuction>().Include(o => o.Images).ThenInclude(o => o.Image).Include(o => o.Seller).Include(o => o.Categories).ThenInclude(o => o.Categorie).FirstOrDefaultAsync(o => o.Id == auctionId);
    }

    public async Task<List<Auction>> All(Auction? lastElement = null) => await _set
        .Include(o => o.Buyer)
        .Where(o => o.Buyer == null)
        .SkipWhile(o => lastElement != null && o.Id != lastElement.Id)
        .Take(20)
        .ToListAsync();

    public async Task<List<Auction>> Filter(string title, List<Categorie> categories, Auction? lastElement = null) {
        return await _set
            .Include(o => o.Categories).ThenInclude(o => o.Categorie)
            .Include(o => o.Buyer)
            .Where(o => o.Buyer == null)
            .Where(o => string.IsNullOrWhiteSpace(title) ? true : o.Title.ToLower().Contains(title.ToLower()))
            .Where(o => categories == null || !categories.Any() || !o.Categories.Any() ? true : o.Categories.Select(o => o.Categorie.Label).Aggregate((a, b) => a + ";" + b) == categories.Select(c => c.Label).Aggregate((a, b) => a + ";" + b))
            .Where(o => o.Id > (lastElement == null ? 0 : lastElement.Id))
            .Take(20)
            .AsSplitQuery()
            .ToListAsync();
    }
    public async Task<List<Categorie>> GetCategories() => await _context.Set<Categorie>().ToListAsync();

    public async Task<List<Auction>> GetSellerProducts(User user) => await _set.Include(o => o.Seller).Where(o => o.Seller.Id == user.Id).ToListAsync();
    public async Task<List<Auction>> GetSimiliarProducts(Auction auction) => await _set
            .Include(o => o.Categories).ThenInclude(o => o.Categorie)
            .Include(o => o.Buyer)
            .Where(o => o.Buyer == null)
            .Where(o => auction.Categories.Select(o => o.Categorie).ToList().Any() || !o.Categories.Any() ? true : o.Categories.Select(o => o.Categorie).Select(c => c.Label).Aggregate((a, b) => a + ";" + b) == auction.Categories.Select(o => o.Categorie).Select(c => c.Label).Aggregate((a, b) => a + ";" + b))
            .Where(o => o.Id != auction.Id)
            .Take(20)
            .ToListAsync();


    public async Task<List<Auction>> GetPanelAuctions(string name) => await _set.Where(o => string.IsNullOrWhiteSpace(name) ? true : o.Title.ToUpper().Contains(name.ToUpper())).Take(20).ToListAsync();

    public async Task<List<BiddingAuctionBid>> GetBids(int auctionId) => await _context.Set<BiddingAuctionBid>().Include(o => o.Auction).Where(o => o.Auction.Id == auctionId).ToListAsync();

    public async Task<bool> Bet(BiddingAuctionBid bid) {
        BiddingAuction auction = await _context.Set<BiddingAuction>().Include(o => o.Seller).Include(o => o.Buyer).Include(o => o.Bidders).ThenInclude(o => o.Bidder).FirstOrDefaultAsync(o => o.Id == bid.Auction.Id);
        if (auction == null || auction.Buyer != null) return false;
        User user = await _context.Set<User>().FirstOrDefaultAsync(o => o.Id == bid.Bidder.Id);
        BiddingAuctionBid? lastBid = auction.Bidders.LastOrDefault();
        decimal minPrice = (lastBid == null ? auction.StartingPrice : lastBid.Price + auction.Step ?? 0);
        if (DateTime.Now > auction.EndDate) return false;
        if (bid.Price < minPrice) return false;
        if (user.Balance < bid.Price) return false;
        if (DateTime.Now > auction.EndDate.AddMinutes(-5)) {
            auction.EndDate = DateTime.Now.AddMinutes(5);
        }

        bid.Auction = null;
        bid.Bidder = user;
        bid.BidDate = DateTime.Now;

        auction.Bidders.Add(bid);

        _context.Set<BiddingAuction>().Update(auction);

        if (lastBid != null) {
            auction.Seller.Balance += bid.Price - lastBid.Price;
            lastBid.Bidder.Balance += lastBid.Price;
            user.Balance -= bid.Price;
            _context.Set<User>().UpdateRange(auction.Seller, lastBid.Bidder, user);
        } else {
            auction.Seller.Balance += bid.Price;
            user.Balance -= bid.Price;
            _context.Set<User>().UpdateRange(auction.Seller, user);
        }
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Buy(int auctionId, int userId, decimal price) {
        Auction a = await Get(auctionId);
        User user = await _context.Set<User>().FirstOrDefaultAsync(o => o.Id == userId);
        if (DateTime.Now > a.EndDate) return false;
        if (user.Balance < price) return false;
        if (a is BiddingAuction bia) {
            if (price < bia.InstantBuyPrice) return false;
            a.FinalPrice = price;
            a.Buyer = user;
            a.Seller.Balance += a.FinalPrice ?? 0;
            a.Buyer.Balance += a.FinalPrice ?? 0;

        } else if (a is BuyAuction bua) {
            if (price < bua.MinimumPrice) return false;
            a.FinalPrice = price;
            a.Buyer = user;
            a.Seller.Balance += a.FinalPrice ?? 0;
            a.Buyer.Balance += a.FinalPrice ?? 0;
        }
        await UpdateAsync(a);
        return true;
    }
}
