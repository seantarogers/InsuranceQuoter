namespace InsuranceQuoter.Infrastructure.Message.Events
{
    using System;

    public class PaymentTakenEvent
    {
        public Guid PaymentUid { get; set; }
        public string CardNumber { get; set; }
    }
}