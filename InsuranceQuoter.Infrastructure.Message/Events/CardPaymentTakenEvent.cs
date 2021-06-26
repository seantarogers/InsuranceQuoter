namespace InsuranceQuoter.Infrastructure.Message.Events
{
    using System;

    public class CardPaymentTakenEvent : Message
    {
        public Guid PaymentUid { get; set; }
    }
}