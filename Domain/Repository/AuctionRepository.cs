using EFCAT.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository;

public interface IAuctionRepository : IRepositoryAsync<Auction> {
    Task<List<Auction>> All(Auction? lastElement = null);
    Task<List<Auction>> Filter(string title, List<Categorie> categories, Auction? lastElement = null);
    Task<List<Categorie>> GetCategories();
    Task<List<Auction>> GetSellerProducts(User user);
    Task<List<Auction>> GetSimiliarProducts(Auction auction);
}

public class AuctionRepository : ARepositoryAsync<Auction>, IAuctionRepository {
    public AuctionRepository(AuctionDbContext context) : base(context) { }

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
            .Where(o => categories == null || !categories.Any() || !o.Categories.Any() ? true : CombineCategories(o.Categories.Select(o => o.Categorie)) == CombineCategories(categories))
            .Where(o => o.Id > (lastElement == null ? 0 : lastElement.Id))
            .Take(20)
            .ToListAsync();
    }
    public async Task<List<Categorie>> GetCategories() => await _context.Set<Categorie>().ToListAsync();
    private string CombineCategories(IEnumerable<Categorie> categories) => categories.Select(c => c.Label).Aggregate((a, b) => a + ";" + b);

    public async Task<List<Auction>> GetSellerProducts(User user) => await _set.Include(o => o.Seller).Where(o => o.Seller.Id == user.Id).ToListAsync();
    public async Task<List<Auction>> GetSimiliarProducts(Auction auction) => await _set
            .Include(o => o.Categories).ThenInclude(o => o.Categorie)
            .Include(o => o.Buyer)
            .Where(o => o.Buyer == null)
            .Where(o => auction.Categories.Select(o => o.Categorie).ToList().Any() || !o.Categories.Any()? true : CombineCategories(o.Categories.Select(o => o.Categorie)) == CombineCategories(auction.Categories.Select(o => o.Categorie).ToList()))
            .Where(o => o.Id != auction.Id)
            .Take(20)
            .ToListAsync();
}
