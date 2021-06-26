namespace InsuranceQuoter.Application.Query.Queries
{
    using System;
    using InsuranceQuoter.Application.Query.Results;

    public record GetRiskByUidQuery(Guid Uid) : Query<RiskByUidResult>;
}