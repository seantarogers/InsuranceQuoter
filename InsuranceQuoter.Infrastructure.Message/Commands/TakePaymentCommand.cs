namespace InsuranceQuoter.Infrastructure.Message.Commands
{
    public class TakePaymentCommand : Message
    {
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string QuoteReference { get; set; }
    }
}