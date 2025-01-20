namespace RP.Shared
{
    public class ChargeRequestModel
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public decimal SubTotalAmount { get; set; }
        public DateTime ChargeDate { get; set; }
    }

    public class ChargeModel : ChargeRequestModel
    {
        public decimal TotalAmount => SubTotalAmount + FeeAmount;
        public decimal FeeAmount { get; set; }
        public decimal NewBalance { get; set; }

        public ChargeModel()
        {
            
        }

        public ChargeModel(ChargeRequestModel request)
        {
            CardId = request.CardId;
            CardNumber = request.CardNumber;
            SubTotalAmount = request.SubTotalAmount;
            ChargeDate = request.ChargeDate;
        }

        public ChargeModel UpdateFeeForCharge(decimal fee)
        {
            FeeAmount = fee;
            return this;
        }

        public ChargeModel UpdateBalance(decimal balance)
        {
            NewBalance = balance;
            return this;
        }
    }
}
