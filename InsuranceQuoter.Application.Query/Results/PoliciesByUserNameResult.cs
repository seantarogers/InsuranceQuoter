namespace InsuranceQuoter.Application.Query.Results
{
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public record PoliciesByUserNameResult(IEnumerable<PolicyDto> Policies) : QueryResult;
}