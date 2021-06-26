namespace InsuranceQuoter.Application.Command.Results
{
    using System;

    public record AddPolicyResult(Guid Uid) : CommandResult;
}