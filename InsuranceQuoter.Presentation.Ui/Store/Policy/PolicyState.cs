namespace InsuranceQuoter.Presentation.Ui.Store.Policy
{
    using System.Collections.Generic;
    using InsuranceQuoter.Presentation.Ui.Models;

    public record PolicyState
    {
        public bool PoliciesRetrieving { get; init; }
        public List<PolicyModel> Models { get; init; }
    }
}