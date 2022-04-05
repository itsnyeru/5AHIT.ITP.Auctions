using EFCAT.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository;

public interface ICategoryRepository : IRepositoryAsync<Categorie> {
    Task<List<Categorie>> GetCategories();
}

public class CategoryRepository : ARepositoryAsync<Categorie>, ICategoryRepository {
    public CategoryRepository(AuctionDbContext context) : base(context) { }

    public async Task<List<Categorie>> GetCategories() => await _set.ToListAsync();
}
