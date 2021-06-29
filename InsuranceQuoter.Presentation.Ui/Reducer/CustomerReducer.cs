namespace InsuranceQuoter.Presentation.Ui.Reducer
{
    using System.Collections.Generic;
    using System.Linq;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Customer;

    public static class CustomerReducer
    {
        [ReducerMethod]
        public static CustomerState Handle(CustomerState state, FindAddressSelectedAction _) =>
            state with
            {
                AddressRetrieving = true,
                Model = state.Model,
                AddressRetrieved = false,
                AddressNotFound = false,
            };

        [ReducerMethod]
        public static CustomerState Handle(CustomerState state, AddressSelected action)
        {
            AddressModel selectedAddress = state.Addresses.Single(a => a.Uid == action.Uid);

            return state with
            {
                Model = new CustomerModel
                {
                    DateOfBirth = state.Model.DateOfBirth,
                    FirstName = state.Model.FirstName,
                    LastName = state.Model.LastName,
                    AddressUid = action.Uid,
                    AddressLine1 = selectedAddress.AddressLine1,
                    AddressLine2 = selectedAddress.AddressLine2,
                    City = selectedAddress.City,
                    County = selectedAddress.County,
                    Postcode = selectedAddress.Postcode
                }
            };
        }

        [ReducerMethod]
        public static CustomerState Handle(CustomerState state, AddressesRetrievedAction action)
        {
            return new()
            {
                AddressNotFound = false,
                Addresses = action.Addresses.Select(
                    a => new AddressModel
                    {
                        Uid = a.Id,
                        AddressLine1 = a.AddressLine1,
                        Postcode = a.Postcode,
                        City = a.City,
                        County = a.County,
                        AddressLine2 = a.AddressLine2
                    }).ToList(),
                AddressRetrieved = true,
                AddressRetrieving = false,
                Model = new CustomerModel
                {
                    DateOfBirth = state.Model.DateOfBirth,
                    FirstName = state.Model.FirstName,
                    LastName = state.Model.LastName,
                    Postcode = state.Model.Postcode
                }
            };
        }

        [ReducerMethod]
        public static CustomerState Handle(CustomerState state, AddressesNotFoundAction _) =>
            state with
            {
                Addresses = new List<AddressModel>(),
                AddressNotFound = true,
                AddressRetrieved = true,
                AddressRetrieving = false
            };

        [ReducerMethod]
        public static CustomerState Handle(CustomerState state, InitializeStateAction _) =>
            new()
            {
                Model = new CustomerModel(),
                AddressRetrieved = false,
                AddressRetrieving = false,
                AddressNotFound = false,
                Addresses = new List<AddressModel>(),
                IsValid = false
            };
    }
}