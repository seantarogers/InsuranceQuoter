namespace InsuranceQuoter.Infrastructure.Message.Events
{
    using System;

    public class PurchaseAndBindCompletedEvent : Message
    {
        public Guid PolicyUid { get; set; }
        public string InsurerName { get; set; }
    }
}