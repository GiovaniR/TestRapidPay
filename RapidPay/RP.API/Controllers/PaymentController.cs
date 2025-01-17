using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RP.Application;
using RP.Payment;
using RP.Shared;

namespace RP.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IPersist _persist;
        private readonly IUniversalFeeExchangeService _feeExchangeService;

        public PaymentController(
            IPaymentService paymentService,
            IPersist persistCard,
            IUniversalFeeExchangeService feeExchangeService)
        {
            _paymentService = paymentService;
            _persist = persistCard;
            _feeExchangeService = feeExchangeService;
        }

        [HttpGet(Name = "Pay")]
        [Authorize]
        public Card Pay(string cardNumber, decimal amount)
        {
            var ufe = _feeExchangeService.CheckAndRefresh();

            var paymentAmount = amount + ufe.Fee;

            var cardToModify = _persist.GetCard(cardNumber);
            var modifiedCard = _paymentService.Pay(cardToModify, paymentAmount);
            
            modifiedCard.SetLastFee(ufe);
            return _persist.SaveCard(modifiedCard);
        }
    }
}
