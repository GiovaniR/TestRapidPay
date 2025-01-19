using RP.Shared;

namespace RP.Infrastructure
{
    public interface ICardRepository 
    {
        Task<Card> SaveAsync(Card card);
        Task<Card> GetCardByNumberAsync(string number);
        Task<List<Card>> GetAllCardsAsync();
    }
    public class CardRepository : ICardRepository
    {
        private readonly IPersist _cardPersist;
        
        public CardRepository(IPersist cardPersist)
        {
            _cardPersist = cardPersist;
        }

        public async Task<Card> SaveAsync(Card card)
        {
            var savedCard = await _cardPersist.SaveCardAsync(card);
            return card;
        }

        public async Task<Card> GetCardByNumberAsync(string number)
        { 
            var cards = await _cardPersist.GetCardByNumberAsync(number);
            return cards;
        }

        public async Task<List<Card>> GetAllCardsAsync()
        {
            var cards = await _cardPersist.GetAllCardsAsync();
            return cards;
        }
    }
}
