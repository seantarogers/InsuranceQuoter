﻿namespace InsuranceQuoter.Presentation.Ui.Reducers
{
    using System.Collections.Generic;
    using System.Linq;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Extensions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Policy;

    public class PolicyReducer
    {
        [ReducerMethod]
        public static PolicyState Handle(PolicyState state, SortPoliciesAscendingByInsurerRequestedAction _) =>
            state with
            {
                Models = state.Models.OrderBy(a => a.Insurer).ToList()
            };

        [ReducerMethod]
        public static PolicyState Handle(PolicyState state, SortPoliciesDescendingByInsurerRequestedAction _) =>
            state with
            {
                Models = state.Models.OrderByDescending(a => a.Insurer).ToList()
            };

        [ReducerMethod]
        public static PolicyState Handle(PolicyState state, SortPoliciesAscendingByDriverNameRequestedAction _) =>
            state with
            {
                Models = state.Models.OrderBy(a => a.DriverName).ToList()
            };

        [ReducerMethod]
        public static PolicyState Handle(PolicyState state, SortPoliciesDescendingByDriverNameRequestedAction _) =>
            state with
            {
                Models = state.Models.OrderByDescending(a => a.DriverName).ToList()
            };

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
                        StartsOn = a.StartsOn.ToNiceDate(),
                        ExpiresOn = a.ExpiresOn.ToNiceDate(),
                        Insurer = a.InsurerName,
                        Registration = a.Registration,
                        PolicyUid = a.Id
                    }).ToList()
            };
        }
    }
}