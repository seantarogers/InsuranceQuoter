namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Customer;
    using Microsoft.AspNetCore.Components;

    public partial class Customer
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IState<CustomerState> CustomerState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        [Inject]
        public SignalRConnectionManager SignalRConnectionManager { get; set; }

        public List<AddressModel> AddressModels => CustomerState.Value.Addresses;
        public CustomerModel Model => CustomerState.Value.Model;
        public bool AddressRetrieved => CustomerState.Value.AddressRetrieved;
        public bool AddressRetrieving => CustomerState.Value.AddressRetrieving;
        public bool AddressNotFound => CustomerState.Value.AddressNotFound;

        public void FindAddressSelected()
        {
            Dispatcher.Dispatch(new FindAddressSelectedAction(Model.Postcode));
        }

        public void AddressSelected(ChangeEventArgs changeEventArgs)
        {
            Dispatcher.Dispatch(new AddressSelected(Guid.Parse(changeEventArgs.Value.ToString())));

            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new InitializeStateAction());

            await SignalRConnectionManager.Initialize();

            await base.OnInitializedAsync();
        }

        private void HandleValidSubmit()
        {
            NavigationManager.NavigateTo("car");
        }
    }
}