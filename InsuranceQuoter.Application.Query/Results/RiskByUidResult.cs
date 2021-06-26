namespace InsuranceQuoter.Application.Query.Results
{
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public record RiskByUidResult(RiskDto RiskDto) : QueryResult;
}