namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Constants;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Policy;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;

    public partial class Policy
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }

        [Inject]
        public IState<PolicyState> PolicyState { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public bool PoliciesRetrieving => PolicyState.Value.PoliciesRetrieving;
        public List<PolicyModel> Policies => PolicyState.Value.Models;

        public void SortByDriverNameAscending()
        {
            Dispatcher.Dispatch(new SortPoliciesAscendingByDriverNameRequestedAction());
        }

        public void SortByDriverNameDescending()
        {
            Dispatcher.Dispatch(new SortPoliciesDescendingByDriverNameRequestedAction());
        }

        public void SortByPolicyUidAscending()
        {
            Dispatcher.Dispatch(new SortPoliciesAscendingByPolicyUidRequestedAction());
        }

        public void SortByPolicyUidDescending()
        {
            Dispatcher.Dispatch(new SortPoliciesDescendingByPolicyUidRequestedAction());
        }

        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new InitializeStateAction());

            AuthenticationState authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            string authenticatedUserEmailAddress = authenticationState.User.Claims.Single(a => a.Type == UiConstants.EmailClaimType).Value;

            Dispatcher.Dispatch(new PoliciesRequestedAction(authenticatedUserEmailAddress));

            await base.OnInitializedAsync();
        }
    }
}