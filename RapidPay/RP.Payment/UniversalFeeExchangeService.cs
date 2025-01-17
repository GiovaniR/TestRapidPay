using RP.Shared;

namespace RP.Payment
{
    public interface IUniversalFeeExchangeService
    {
        double GetFeeAmount();
    }

    public class UniversalFeeExchangeService : IUniversalFeeExchangeService
    {
        public double GetFeeAmount()
        { 
            Random random = new Random();
            return random.NextDouble() * (2 - 0) + 0;
        }

    }
}
