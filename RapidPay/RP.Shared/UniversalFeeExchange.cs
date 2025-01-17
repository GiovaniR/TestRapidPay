namespace RP.Shared
{
    public class UniversalFeeExchange
    {
        public decimal Fee { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public UniversalFeeExchange(decimal fee, DateTime current, DateTime expire)
        {
            Fee = fee;
            GeneratedAt = current;
            ExpiresAt = expire;
        }
    }
}
