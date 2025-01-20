using Microsoft.AspNetCore.Mvc.Testing;
using RP.Shared;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace RP.API.Test
{
    public class PaymentIntegrationTests : 
        IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public PaymentIntegrationTests(CustomWebApplicationFactory<Program> factory) 
        { 
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions { 
                AllowAutoRedirect = false
            });
        }

        public async Task ProcessPayment_ReturnsSuccess_WhenValidDataIsProvided()
        {
            var user = new UserModel() { 
                Username = "Test",
                Password = "Password"
            };

            var authorizationResponse = await _client.PostAsJsonAsync("/Authorization/Create", user);
            var tokenResponse = await authorizationResponse.Content.ReadFromJsonAsync<TokenResponse>();
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token);

            var createCardResponse = await _client.PostAsJsonAsync("/Card/CreateCard", new { });
            var card = await createCardResponse.Content.ReadFromJsonAsync<Card>();

            var charge = new ChargeRequestModel()
            {
                CardNumber = card.Number,
                ChargeDate = DateTime.Now,
                SubTotalAmount = 100
            };

            var chargeResponse = await _client.PostAsJsonAsync("/Payment/Pay", charge);
            var payment = await chargeResponse.Content.ReadFromJsonAsync<ChargeModel>();

            Assert.Equal(HttpStatusCode.OK, authorizationResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, createCardResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, chargeResponse.StatusCode);
        }

        public class TokenResponse { 
            public string Token { get; set; }
        }
    }
}
