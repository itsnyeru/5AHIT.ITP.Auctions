using EFCAT.Domain.Repository;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository {
    public interface IAuctionRepository : IRepositoryAsync<Auction> { }

    public class AuctionRepository : ARepositoryAsync<Auction>, IAuctionRepository {
        public AuctionRepository(AuctionDbContext context) : base(context) {  }
    }
}
