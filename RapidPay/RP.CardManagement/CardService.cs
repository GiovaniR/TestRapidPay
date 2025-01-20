using RP.Infrastructure;
using RP.Shared;

namespace RP.Application
{
    public interface ICardService
    {
        Task<Card> CreateCardAsync();
        Task<Card> GetCardByNumberAsync(string number);
        Task<List<Card>> GetAllCardsAsync();
        Task<Card> SaveAsync(Card card);
        Task<Card> GetCardByIdAsync(int id);
    }

    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<Card> CreateCardAsync()
        {
            var cardNumber = string.Empty;
            Random random = new Random();

            for (int i = 0; i < 15; i++)
            {
                cardNumber += random.Next(0, 10).ToString();
            }

            var newCard = new Card(cardNumber, SetCardBalance());
            //await _cardRepository.AddAsync(newCard);
            await _cardRepository.AddOrUpdateAsync(newCard);
            return newCard;
        }

        private static decimal SetCardBalance()
        {
            return 1000;
        }

        public async Task<Card> GetCardByIdAsync(int id)
        { 
            var card = await _cardRepository.GetCardByIdAsync(id);
            return card;
        }

        public async Task<Card> GetCardByNumberAsync(string number)
        {
            var card = await _cardRepository.GetCardByNumberAsync(number);
            return card;
        }

        public async Task<List<Card>> GetAllCardsAsync()
        {
            var cards = await _cardRepository.GetAllCardsAsync();
            return cards.ToList();
        }

        public async Task<Card> SaveAsync(Card card)
        {
            var exists = _cardRepository.GetCardByNumberAsync(card.Number);
            var newCard = await _cardRepository.AddOrUpdateAsync(card);
            return newCard;
        }


    }
}
