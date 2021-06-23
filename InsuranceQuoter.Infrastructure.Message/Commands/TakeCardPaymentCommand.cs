namespace InsuranceQuoter.Infrastructure.Message.Commands
{
    public class TakeCardPaymentCommand : Message
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}