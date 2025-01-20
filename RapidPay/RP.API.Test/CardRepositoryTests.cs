using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RP.Infrastructure;
using RP.Shared;
using Xunit;

namespace RP.API.Test
{
    public class CardRepositoryTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly DbContextOptions<ApplicationDbContext> _repo;

        public CardRepositoryTests(CustomWebApplicationFactory<Program> factory)
        {
            _repo = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;
        }

        [Fact]
        public async Task SaveCard_AddsNewCard_WhenNotExists()
        {
            using (var context = new ApplicationDbContext(_repo)) 
            { 
                await context.Database.OpenConnectionAsync();
                context.Database.EnsureCreated();

                var repository = new CardRepository(context);
                var newCard = new Card("123456789012345", 1000);
                var savedCard = await repository.AddOrUpdateAsync(newCard);

                Assert.NotNull(savedCard);
                Assert.Equal("123456789012345", savedCard.Number);
            }
            
        }
    }
}
