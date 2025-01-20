using RP.Application;
using RP.Shared;

namespace RP.External
{
    public interface IPaymentService
    {
        Task<ChargeModel> PayAsync(ChargeModel charge);
        Task<ChargeModel> PayWithIdAsync(ChargeModel charge);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IUniversalFeeExchangeService _feeExchangeService;
        private readonly ICardService _cardService;

        public PaymentService(
            IUniversalFeeExchangeService universalFeeExchangeService,
            ICardService cardService)
        {
            _feeExchangeService = universalFeeExchangeService;
            _cardService = cardService;
        }

        public async Task<ChargeModel> PayWithIdAsync(ChargeModel charge)
        {
            var card = await _cardService.GetCardByIdAsync(charge.CardId);
            charge.CardNumber = card.Number;
            var chargeResult = await PayAsync(charge);
            return chargeResult;
        }

        public async Task<ChargeModel> PayAsync(ChargeModel charge)
        {
            var ufe = _feeExchangeService.CheckAndRefresh();
            charge.UpdateFeeForCharge(ufe);

            var card = await _cardService.GetCardByNumberAsync(charge.CardNumber);
            var modifiedCard = ChargeCard(card, charge);

            charge.UpdateBalance(modifiedCard.Balance);
            await _cardService.SaveAsync(modifiedCard);

            return charge;
        }

        private Card ChargeCard(Card card, ChargeModel charge)
        {
            if (card.Balance >= charge.TotalAmount)
            {
                card.ChargeCard(charge.TotalAmount);
                card.SetLastFee(charge.FeeAmount);
                return card;
            }

            throw new ArgumentException("Insuficient Amount");
        }
    }
}
