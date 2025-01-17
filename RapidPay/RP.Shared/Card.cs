namespace RP.Shared
{
    public class Card
    {
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public decimal OriginalAmount { get; }

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
    }
}