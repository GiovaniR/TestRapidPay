using RP.Shared;

namespace RP.Infrastructure
{
    public interface IPersist
    {
        Card SaveCard(Card card);
        List<Card> GetSavedCards();
        Card GetCard(string cardNumber);
        UniversalFeeExchange SaveFee(UniversalFeeExchange fee);
        UniversalFeeExchange GetFee();
    }

    public class Persist : IPersist
    {
        List<Card> Cards = new List<Card>();
        UniversalFeeExchange Fee;

        public Card SaveCard(Card card)
        {
            var index = FindCardIndex(card.Number);

            if (index != -1)
                Cards[index] = card;
            else
                Cards.Add(card);
            
            return card;
        }

        public List<Card> GetSavedCards()
        {
            return Cards.ToList();
        }

        public Card GetCard(string cardNumber)
        {
            return Cards.Single(x => x.Number == cardNumber);
        }

        public int FindCardIndex(string cardNumber)
        {
            return Cards.FindIndex(x => x.Number == cardNumber);
        }

        public UniversalFeeExchange SaveFee(UniversalFeeExchange fee)
        { 
            Fee = fee;
            return Fee;
        }

        public UniversalFeeExchange GetFee() 
        { 
            return Fee;
        }
    }
}
