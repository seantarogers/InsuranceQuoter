namespace InsuranceQuoter.Presentation.Ui.Reducer
{
    using System.Collections.Generic;
    using System.Linq;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Policy;

    public class PolicyReducer
    {
        [ReducerMethod]
        public static PolicyState Handle(PolicyState _, InitializeStateAction __) =>
            new()
            {
                Models = new List<PolicyModel>()
            };

        [ReducerMethod]
        public static PolicyState Handle(PolicyState state, PoliciesRequestedAction _) =>
            state with
            {
                PoliciesRetrieving = true
            };

        [ReducerMethod]
        public static PolicyState Handle(PolicyState state, PoliciesRetrievedAction action)
        {
            return new()
            {
                PoliciesRetrieving = false,
                Models = action.Policies.Select(
                    a => new PolicyModel
                    {
                        CoverType = a.CoverType,
                        DriverName = a.FirstName + " " + a.LastName,
                        ExpiresOn = a.ExpiresOn.ToString("dddd, dd MMMM yyyy"),
                        Insurer = a.InsurerName,
                        Registration = a.Registration,
                        PolicyUid = a.Id
                    }).ToList()
            };
        }
    }
}