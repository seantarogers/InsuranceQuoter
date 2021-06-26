namespace InsuranceQuoter.Infrastructure.Message.Events
{
    using System;

    public class RiskAddedEvent : Message
    {
        public Guid RiskUid { get; set; }
    }
}