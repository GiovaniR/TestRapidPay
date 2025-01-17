using RP.Shared;
using System.ComponentModel.DataAnnotations;

namespace RP.Payment
{
    public interface IPaymentService
    {
        Card Pay(Card card);
    }

    public class PaymentService : IPaymentService
    {
        public Card Pay(Card card)
        {
            Random random = new Random();
            var chargeAmount = random.Next(0,1000);

            if (card.Balance > chargeAmount)
                card.ChargeCard(chargeAmount);

            return card;
        }
    }
}
