using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RP.External;
using RP.Shared;

namespace RP.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(
            IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost(Name = "Pay")]
        [Authorize]
        public async Task<IActionResult> PayAsync([FromBody] ChargeRequestModel chargeDto)
        {
            var charge = new ChargeModel(chargeDto);
            var chargeAfterPay = await _paymentService.PayAsync(charge);
            
            return Ok(chargeAfterPay);
        }
    }
}
