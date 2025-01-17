using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RP.Application;
using RP.Shared;

namespace RP.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CardManagementController : ControllerBase
    {
        private readonly ICardManagmentService _cardManagmentService;
        private readonly IPersist _persistCard;

        public CardManagementController(
            ICardManagmentService cardService,
            IPersist persistCard)
        {
            _cardManagmentService = cardService;
            _persistCard = persistCard;
        }

        [HttpGet(Name = "CreateCard")]
        [Authorize]
        public string Create()
        {
            var newCard = _cardManagmentService.CreateCard();
            _persistCard.SaveCard(newCard);

            return newCard.Number;
        }

        [HttpGet(Name = "GetCards")]
        [Authorize]
        public List<Card> GetAll()
        { 
            return _persistCard.GetSavedCards();
        }

        [HttpGet(Name = "GetBalance")]
        [Authorize]
        public Card GetBalance(string number)
        {
            return _persistCard.GetCard(number);
        }
    }
}
