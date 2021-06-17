namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using System;
    using System.Collections.Generic;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Customer;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;

    public partial class Customer
    {
        //private EditContext EditContext;
        public string SelectedAddress { get; set; } = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IState<CustomerState> CustomerState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public List<AddressModel> AddressModels => CustomerState.Value.Addresses;
        public CustomerModel Model => CustomerState.Value.Model;
        public bool AddressRetrieved => CustomerState.Value.AddressRetrieved;
        public bool AddressRetrieving => CustomerState.Value.AddressRetrieving;

        private void HandleValidSubmit()
        {
            NavigationManager.NavigateTo("car");
        }

        public void FindAddressSelected()
        {
            Dispatcher.Dispatch(new FindAddressSelectedAction(Model.Postcode));
        }

        public void AddressSelected(ChangeEventArgs changeEventArgs)
        {
            Dispatcher.Dispatch(new AddressSelected(Guid.Parse(changeEventArgs.Value.ToString())));

            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            Dispatcher.Dispatch(new CustomerInitializationRequestedAction(Model));

            base.OnInitialized();
        }
    }
}