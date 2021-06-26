namespace InsuranceQuoter.Infrastructure.Message.Commands
{
    using System;

    public class AddPolicyCommand : Message
    {
        public string Username { get; set; }
        public Guid PolicyUid { get; set; }
        public Guid PaymentUid { get; set; }
        public Guid QuoteUid { get; set; }
        public Guid RiskUid { get; set; }
        public string InsurerName { get; set; }
        public string Addons { get; set; }
    }
}