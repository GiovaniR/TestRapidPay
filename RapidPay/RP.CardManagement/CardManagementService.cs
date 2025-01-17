using RP.Shared;

namespace RP.Application
{
    public interface ICardManagmentService
    {
        Card CreateCard();
    }

    public class CardManagementService : ICardManagmentService
    {
        public Card CreateCard()
        {
            var cardNumber = string.Empty;
            Random random = new Random();

            for (int i = 0; i < 15; i++)
            {
                cardNumber += random.Next(0, 10).ToString();
            }

            return new Card(cardNumber, SetCardBalance());
        }

        private static decimal SetCardBalance()
        {
            return 1000;
        }
    }
}
