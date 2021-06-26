namespace InsuranceQuoter.Application.Command.Results
{
    using System;

    public record AddRiskResult(Guid RiskUid) : CommandResult;
}