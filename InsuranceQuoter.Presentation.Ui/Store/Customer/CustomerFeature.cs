namespace InsuranceQuoter.Presentation.Ui.Store.Customer
{
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Models;

    public class CustomerFeature : Feature<CustomerState>
    {
        public override string GetName() => nameof(CustomerState);

        protected override CustomerState GetInitialState()
        {
            return new CustomerState
            {
                AddressRetrieved = false,
                AddressRetrieving = false,
                IsValid = false,
                Model = new CustomerModel()
            };
        }
    }
}