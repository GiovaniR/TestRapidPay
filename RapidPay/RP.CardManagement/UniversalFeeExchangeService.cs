using RP.Shared;

namespace RP.Application
{
    public interface IUniversalFeeExchangeService
    {
        decimal GenerateFee();
        decimal CheckAndRefresh();
    }

    public class UniversalFeeExchangeService : IUniversalFeeExchangeService
    {
        private readonly TimeSpan _expiresAt;
        private DateTime _lastRefreshTime;
        public decimal CurrentFee { get; private set; }

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

        public decimal CheckAndRefresh()
        {
            //if (DateTime.UtcNow - _lastRefreshTime > _expiresAt)
            if(true)
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

        public decimal GenerateFee()
        {
            var fee = Convert.ToDecimal(GetFeeAmount());

            if (CurrentFee > 0)
            {
                CurrentFee = CurrentFee * fee;
            }
            else
            { 
                CurrentFee = fee;
            }

            return CurrentFee;
        }
    }
}
