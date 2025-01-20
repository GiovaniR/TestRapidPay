using Microsoft.Extensions.DependencyInjection;
using RP.Infrastructure;
using RP.Shared;
using Xunit;

namespace RP.API.Test
{
    public class CardRepositoryTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly ICardRepository _cardRepository;
        private readonly ApplicationDbContext _context;

        public CardRepositoryTests(CustomWebApplicationFactory<Program> factory)
        {
            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _cardRepository = scope.ServiceProvider.GetRequiredService<ICardRepository>();
        }

        [Fact]
        public async Task SaveCard_AddsNewCard_WhenNotExists()
        {
            var newCard = new Card("123456789012345", 1000);
            var savedCard = await _cardRepository.AddOrUpdateAsync(newCard);
            
            Assert.NotNull(savedCard);
            Assert.Equal("123456789012345", savedCard.Number);

            var searchedCard = await _context.Cards.FindAsync(savedCard.Id);
            Assert.NotNull(searchedCard);
        }

        [Fact]
        public async Task SaveCard_UpdatesExistingCard_WhenExists()
        {
            var existingCard = new Card("123456789012345", 1000) { Id = 1};

            await _cardRepository.AddOrUpdateAsync(existingCard);

            existingCard.Balance = 99;

            var updatedCard = await _cardRepository.AddOrUpdateAsync(existingCard);

            Assert.NotNull(updatedCard);
            Assert.Equal(99, updatedCard.Balance);

            var searchedCard = await _context.Cards.FindAsync(updatedCard.Id);
            Assert.Equal(99, searchedCard.Balance);
        }
    }
}
