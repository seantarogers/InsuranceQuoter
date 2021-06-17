namespace InsuranceQuoter.Infrastructure.Message.Commands
{
    public class TakePaymentCommand
    {
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string Reference { get; set; }
    }
}