namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Car;
    using Microsoft.AspNetCore.Components;

    public partial class Car
    {
        [Inject]
        public IState<CarState> CarState { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public TimerManager TimerManager { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public CarModel Model => CarState.Value.Model;
        public bool CarRetrieved => CarState.Value.CarRetrieved;
        public bool CarRetrieving => CarState.Value.CarRetrieving;

        public void FindCarSelected()
        {
            Dispatcher.Dispatch(new FindCarSelectedAction(Model.Registration));
        }
        
        private void HandleValidSubmit()
        {
            NavigationManager.NavigateTo("quotes");

            const int NumberOfTicks = 10;

            TimerManager.Initialize(NumberOfTicks);
            
            Dispatcher.Dispatch(new AllRiskCapturedAction());
        }
    }
}