using EFCAT.Model.Configuration;
using Microsoft.EntityFrameworkCore;
using Model.Entity;

namespace Model.Configuration {
    public class AuthenticationDbContext : AuctionDbContext {

        public AuthenticationDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }
    }
}
