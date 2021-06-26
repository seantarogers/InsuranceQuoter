namespace InsuranceQuoter.Presentation.Ui.Actions
{
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public record PoliciesRetrievedAction(List<PolicyDto> Policies);
}