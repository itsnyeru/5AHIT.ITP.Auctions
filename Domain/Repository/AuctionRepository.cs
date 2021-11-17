using EFCAT.Repository;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository {
    public interface IAuctionRepository : IRepositoryAsync<Auction, int> { }

    public class AuctionRepository : ARepositoryAsync<Auction, int>, IAuctionRepository {
        public AuctionRepository(AuctionDbContext context) : base(context) {  }
    }
}
