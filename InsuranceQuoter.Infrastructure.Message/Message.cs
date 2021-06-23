namespace InsuranceQuoter.Infrastructure.Message
{
    using System;

    public abstract class Message
    {
        public Guid CorrelationId { get; set; }
    }
}