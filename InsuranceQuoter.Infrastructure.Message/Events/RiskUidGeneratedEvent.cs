namespace InsuranceQuoter.Infrastructure.Message.Events
{
    using System;

    public class RiskUidGeneratedEvent : Message
    {
        public Guid RiskUid { get; set; }
    }
}