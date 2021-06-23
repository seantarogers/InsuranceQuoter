namespace InsuranceQuoter.Infrastructure.Message.Responses
{
    using System;
    using System.Collections.Generic;

    public class QuoteResponse
    {
        public Guid Uid { get; set; }
        public string Insurer { get; set; }
        public decimal Premium { get; set; }
        public DateTime StartDate { get; set; }
        public decimal PremiumTax { get; set; }
        public List<string> Addons { get; set; }
    }
}