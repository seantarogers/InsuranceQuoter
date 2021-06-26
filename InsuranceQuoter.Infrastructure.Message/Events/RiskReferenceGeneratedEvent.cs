namespace InsuranceQuoter.Infrastructure.Message.Events
{
    using System;

    public class RiskReferenceGeneratedEvent : Message
    {
        public Guid RiskReference { get; set; }
    }
}