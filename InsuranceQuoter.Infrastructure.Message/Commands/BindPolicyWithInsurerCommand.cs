namespace InsuranceQuoter.Infrastructure.Message.Commands
{
    using System;

    public class BindPolicyWithInsurerCommand : Message
    {
        public Guid QuoteUid { get; set; }
        public string InsurerName { get; set; }
    }
}