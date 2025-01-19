using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RP.API.Controllers;
using RP.Application;
using RP.Infrastructure;
using RP.Shared;

namespace RP.API.Test
{
    public class CardControllerTests
    {
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly CardController _controller;

        public CardControllerTests()
        {
            _cardServiceMock = new Mock<ICardService>();
            _controller = new CardController(_cardServiceMock.Object);
        }

        [Fact]
        public async Task CreateCard_ReturnsCreatedResult()
        {
            var createdCard = new Card("123456789012345", 1000);

            _cardServiceMock.Setup(s=>s.CreateCardAsync()).ReturnsAsync(createdCard);

            var result = await _controller.CreateCardAsync();

            var actionResult = result as OkObjectResult;
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(200);

            actionResult.Value.Should().BeEquivalentTo(createdCard);
        }

        [Fact]
        public async Task GetCard_ReturnsNotFound_WhenCardDoesNotExist()
        {
            
        }

        [Fact]
        public async Task GetCard_ReturnsOf()
        { 

        }
    }
}
