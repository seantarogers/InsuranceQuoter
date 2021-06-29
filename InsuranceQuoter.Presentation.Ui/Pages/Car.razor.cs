namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using System.Linq;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Constants;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Car;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;

    public partial class Car
    {
        [Inject]
        public IState<CarState> CarState { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public TimerManager TimerManager { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public CarModel Model => CarState.Value.Model;
        public bool CarRetrieved => CarState.Value.CarRetrieved;
        public bool CarRetrieving => CarState.Value.CarRetrieving;
        public bool CarNotFound => CarState.Value.CarNotFound;

        public void FindCarSelected()
        {
            Dispatcher.Dispatch(new FindCarSelectedAction(Model.Registration));
        }

        private async Task HandleValidSubmit()
        {
            NavigationManager.NavigateTo("quotes");

            const int NumberOfTicks = 10;
            TimerManager.Initialize(NumberOfTicks);

            AuthenticationState authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            string authenticatedUserEmailAddress = authenticationState.User.Claims.Single(a => a.Type == UiConstants.EmailClaimType).Value;

            Dispatcher.Dispatch(new AllRiskCapturedAction(authenticatedUserEmailAddress));
        }
    }
}