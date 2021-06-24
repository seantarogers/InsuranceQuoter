namespace InsuranceQuoter.Infrastructure.Message.Events
{
    using System;

    public class AllQuotesRetrievedEvent : Message
    {
        public Guid RiskReference { get; set; }
    }
}