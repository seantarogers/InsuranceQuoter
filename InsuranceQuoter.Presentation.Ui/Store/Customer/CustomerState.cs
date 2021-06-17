namespace InsuranceQuoter.Presentation.Ui.Store.Customer
{
    using System.Collections.Generic;
    using InsuranceQuoter.Presentation.Ui.Models;

    public record CustomerState
    {
        public bool AddressRetrieving { get; init; }
        public bool AddressRetrieved { get; init; }
        public bool IsValid { get; init; }
        public CustomerModel Model { get; init; }
        public List<AddressModel> Addresses { get; init; }
    }
}