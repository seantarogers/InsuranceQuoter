namespace InsuranceQuoter.Infrastructure.Contract.Responses
{
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Contract.Dtos;

    public class QuoteResponse
    {
        public string Insurer { get; set; }
        public string Premium { get; set; }
        public string StartDate { get; set; }
        public List<AddonDto> Addons { get; set; }
    }
}