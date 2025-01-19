using RP.Shared;

namespace RP.Infrastructure
{
    public interface IPersist
    {
        Task<List<Card>> SaveCardAsync(Card card);
        Task<Card> GetCardByNumberAsync(string cardNumber);
        Task<List<Card>> GetAllCardsAsync();
    }

    public class Persist : IPersist
    {
        List<Card> Cards = new List<Card>();

        public async Task<List<Card>> SaveCardAsync(Card card)
        {
            var index = FindCardIndex(card.Number);

            if (index != -1)
                Cards[index] = card;
            else
                Cards.Add(card);
            
            return Cards;
        }

        public Task<Card> GetCardByNumberAsync(string cardNumber)
        {
            var card = Cards.Single(x => x.Number == cardNumber);
            return Task.FromResult(card);
        }

        public Task<List<Card>> GetAllCardsAsync() => Task.FromResult(Cards.ToList());

        private int FindCardIndex(string cardNumber)
        {
            return Cards.FindIndex(x => x.Number == cardNumber);
        }
    }
}
