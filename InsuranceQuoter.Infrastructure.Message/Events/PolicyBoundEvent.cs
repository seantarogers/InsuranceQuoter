namespace InsuranceQuoter.Infrastructure.Message.Events
{
    using System;

    public class PolicyBoundEvent : Message
    {
        public Guid PolicyUid { get; set; }
    }
}