using Microsoft.EntityFrameworkCore;
using RP.Shared;

namespace RP.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
    }
}
