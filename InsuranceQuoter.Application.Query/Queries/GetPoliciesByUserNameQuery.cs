namespace InsuranceQuoter.Application.Query.Queries
{
    using InsuranceQuoter.Application.Query.Results;

    public record GetPoliciesByUserNameQuery(string UserName) : Query<PoliciesByUserNameResult>;
}