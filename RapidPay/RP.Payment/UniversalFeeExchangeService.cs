using RP.Shared;
using System.Runtime.InteropServices;

namespace RP.Payment
{
    public interface IUniversalFeeExchangeService
    {
        UniversalFeeExchange GenerateFee();
        UniversalFeeExchange CheckAndRefresh();
    }

    public class UniversalFeeExchangeService : IUniversalFeeExchangeService
    {
        private readonly TimeSpan _expiresAt;
        private DateTime _lastRefreshTime;
        public UniversalFeeExchange CurrentFee { get; private set; }

        public UniversalFeeExchangeService(TimeSpan expiresAt)
        {
            _expiresAt = expiresAt;
            RefreshFee();
        }

        public void RefreshFee()
        { 
            CurrentFee = GenerateFee();
            _lastRefreshTime = DateTime.UtcNow;
        }

        public UniversalFeeExchange CheckAndRefresh()
        {
            if (DateTime.UtcNow - _lastRefreshTime > _expiresAt)
            { 
                RefreshFee();
            }

            return CurrentFee;
        }

        private double GetFeeAmount()
        { 
            Random random = new Random();
            return random.NextDouble() * (2 - 0) + 0;
        }

        public UniversalFeeExchange GenerateFee()
        { 
            var fee = Convert.ToDecimal(GetFeeAmount());

            if (CurrentFee != null)
            { 
                fee = CurrentFee.Fee * fee;
            }

            var currentDate = DateTime.UtcNow;
            var expireDate = DateTime.UtcNow.AddHours(1);
            return new UniversalFeeExchange(fee, currentDate, expireDate);
        }
    }
}
