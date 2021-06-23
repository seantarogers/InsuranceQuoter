namespace InsuranceQuoter.Infrastructure.Message.Events
{
    public class CardPaymentTakenEvent : Message
    {
        public string PaymentReference { get; set; }
    }
}