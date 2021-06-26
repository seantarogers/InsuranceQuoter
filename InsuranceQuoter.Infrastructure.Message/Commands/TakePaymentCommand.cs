namespace InsuranceQuoter.Infrastructure.Message.Commands
{
    using System;

    public class TakePaymentCommand : Message
    {
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public Guid QuoteUid { get; set; }
        public Guid RiskUid { get; set; }
        public string UserName { get; set; }
        public string Addons { get; set; }
        public string InsurerName { get; set; }
    }
}