using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RP.Application;

namespace RP.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost(Name = "CreateCardAsync")]
        [Authorize]
        public async Task<IActionResult> CreateCardAsync()
        {
            var newCard = await _cardService.CreateCardAsync();
            return Ok(newCard);
        }

        [HttpGet(Name = "GetAllAsync")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        { 
            var cards = await _cardService.GetAllCardsAsync();
            return Ok(cards);
        }

        [HttpGet(Name = "GetCardByNumberAsync")]
        [Authorize]
        public async Task<IActionResult> GetCardByNumberAsync(string number)
        {
            var card = await _cardService.GetCardByNumberAsync(number);
            return Ok(card);
        }
    }
}
