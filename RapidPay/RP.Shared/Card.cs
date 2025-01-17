namespace RP.Shared
{
    public class Card
    {
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public decimal OriginalAmount { get; }
        public decimal LastFee { get; set; }

        public Card(string number, decimal originalAmount)
        {
            Number = number;
            Balance = originalAmount;
            OriginalAmount = originalAmount;
        }

        public Card ChargeCard(decimal amount)
        { 
            Balance -= amount;
            return this;
        }

        public Card SetLastFee(UniversalFeeExchange fee)
        {
            LastFee = fee.Fee;
            return this;
        }
    }
}