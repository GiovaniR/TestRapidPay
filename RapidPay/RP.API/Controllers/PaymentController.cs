using Microsoft.AspNetCore.Mvc;
using RP.CardManagement;
using RP.Infrastructure;
using RP.Payment;
using RP.Shared;

namespace RP.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IPersist _persistCard;

        public PaymentController(
            IPaymentService paymentService,
            IPersist persistCard)
        {
            _paymentService = paymentService;
            _persistCard = persistCard;
        }

        [HttpGet(Name = "Pay")]
        public Card Pay(string cardNumber, decimal amount)
        { 
            var cardToModify = _persistCard.GetCard(cardNumber);
            var modifiedCard = _paymentService.Pay(cardToModify);
            return _persistCard.SaveCard(modifiedCard);
        }
    }
}
