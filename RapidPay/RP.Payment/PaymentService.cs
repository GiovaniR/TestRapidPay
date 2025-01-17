using RP.Shared;
using System.ComponentModel.DataAnnotations;

namespace RP.Payment
{
    public interface IPaymentService
    {
        Card Pay(Card card, decimal chargeAmount);
    }

    public class PaymentService : IPaymentService
    {
        public Card Pay(Card card, decimal chargeAmount)
        {
            Random random = new Random();

            if (card.Balance > chargeAmount)
                card.ChargeCard(chargeAmount);

            return card;
        }
    }
}
