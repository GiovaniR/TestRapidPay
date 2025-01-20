using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
            if (string.IsNullOrEmpty(chargeDto.CardNumber))
                return BadRequest("Missing Card");

            try
            {
                var charge = new ChargeModel(chargeDto);
                var chargeAfterPay = await _paymentService.PayAsync(charge);

                return Ok(chargeAfterPay);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Insufficient funds");
            }
            catch (Exception ex)
            {
                return BadRequest("Please try again later.");
            }
        }

        [HttpPost(Name = "PayWithId")]
        [Authorize]
        public async Task<IActionResult> PayWithCardIdAsync([FromBody] ChargeRequestModel chargeDto)
        {
            try
            {
                var charge = new ChargeModel(chargeDto);
                var chargeAfterPay = await _paymentService.PayWithIdAsync(charge);

                return Ok(chargeAfterPay);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Insufficient funds");
            }
            catch (Exception ex)
            {
                return BadRequest("Please try again later.");
            }
        }
    }
}
