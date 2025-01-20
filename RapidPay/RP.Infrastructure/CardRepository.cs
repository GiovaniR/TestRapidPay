using Microsoft.EntityFrameworkCore;
using RP.Shared;

namespace RP.Infrastructure
{
    public interface ICardRepository 
    {
        //Task<Card> AddAsync(Card card);
        //Task<Card> UpdateAsync(Card card);
        Task<Card> GetCardByNumberAsync(string number);
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<Card> GetCardByIdAsync(int id);
        Task<Card> AddOrUpdateAsync(Card card);
    }
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;
        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Card> AddOrUpdateAsync(Card card)
        {
            using (var uow = new UnitOfWork(_context))
            {
                await uow.BeginTransactionAsync();

                try
                {
                    var exists = GetCardByNumberAsync(card.Number);
                    if (exists != null)
                    {
                        _context.Cards.Update(card);
                    }
                    else
                    { 
                        await _context.AddAsync(card);
                    }
                    
                    await _context.SaveChangesAsync();

                    await uow.CommitAsync();
                    return card;
                }
                catch (Exception ex)
                {
                    await uow.RollbackAsync();
                    throw new Exception("Error while updating the Card", ex);
                }
            }
        }

        public async Task<Card> AddAsync(Card card)
        {
            try
            {
                await _context.AddAsync(card);
                await _context.SaveChangesAsync();
                return card;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while Creating the Card", ex);
            }
            
        }

        public async Task<Card> UpdateAsync(Card card)
        {
            using (var uow = new UnitOfWork(_context))
            { 
                await uow.BeginTransactionAsync();

                try
                {
                    _context.Cards.Update(card);
                    await _context.SaveChangesAsync();

                    uow.CommitAsync();
                    return card;
                }
                catch (Exception ex)
                {
                    await uow.RollbackAsync();
                    throw new Exception("Error while updating the Card", ex);
                }
            }
        }

        public async Task<Card> GetCardByNumberAsync(string number)
        {
            var card = await _context.Cards.SingleOrDefaultAsync(c=>c.Number == number);
            return card;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            var cards = await _context.Cards.ToListAsync();
            return cards;
        }

        public async Task<Card> GetCardByIdAsync(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            return card;
        }
    }
}
