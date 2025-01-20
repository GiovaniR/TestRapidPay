using Microsoft.EntityFrameworkCore;
using RP.Shared;

namespace RP.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }

        public async virtual Task<Card> SaveCardAsync(Card card)
        {
            var exists = await Cards.SingleOrDefaultAsync(c=>c.Number == card.Number);

            if (exists != null)
            {
                Cards.Entry(exists).CurrentValues.SetValues(card);
                await SaveChangesAsync();
                return exists;
            }
                
            await Cards.AddAsync(card);
            await SaveChangesAsync();

            return card;
        }

        public async virtual Task<Card> GetCardByNumberAsync(string number)
        {
            return await Cards.SingleAsync(c=>c.Number == number);
        }

        public async virtual Task<List<Card>> GetAllCardsAsync()
        {
            return await Cards.AsNoTracking().ToListAsync();
        }
    }
}
