using System.ComponentModel.DataAnnotations;

namespace RP.Shared
{
    public class Card
    {
        [StringLength(maximumLength: 15, MinimumLength = 15)]
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

        public Card SetLastFee(decimal fee)
        {
            LastFee = fee;
            return this;
        }
    }
}