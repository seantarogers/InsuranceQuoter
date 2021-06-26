namespace InsuranceQuoter.Infrastructure.Message.Responses
{
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class PoliciesResponse
    {
        public List<PolicyDto> Policies { get; set; }
    }
}