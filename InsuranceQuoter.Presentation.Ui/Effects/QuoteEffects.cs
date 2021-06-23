namespace InsuranceQuoter.Presentation.Ui.Effects
{
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Car;
    using InsuranceQuoter.Presentation.Ui.Store.Customer;
    using Microsoft.AspNetCore.SignalR.Client;

    public class QuoteEffects
    {
        private readonly IState<CarState> carState;
        private readonly IState<CustomerState> customerState;

        public QuoteEffects(IState<CarState> carState, IState<CustomerState> customerState)
        {
            this.carState = carState;
            this.customerState = customerState;
        }

        [EffectMethod]
        public Task Handle(AllRiskCapturedAction action, IDispatcher dispatcher)
        {
            CustomerModel customer = customerState.Value.Model;
            CarModel car = carState.Value.Model;

            return SignalRConnectionManager.HubConnection.SendAsync(
                "HandleQuotesRequestAsync",
                new QuoteRequest
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DateOfBirth = customer.DateOfBirth.GetValueOrDefault(),
                    AddressLine1 = customer.AddressLine1,
                    AddressLine2 = customer.AddressLine2,
                    City = customer.City,
                    County = customer.County,
                    Postcode = customer.Postcode,
                    AddressUid = customer.AddressUid,
                    CoverType = car.CoverType,
                    CarId = car.Id,
                    Model = car.Model,
                    Registration = car.Registration,
                    Fuel = car.Fuel,
                    Make = car.Make,
                    Mileage = car.Mileage.GetValueOrDefault(),
                    Transmission = car.Transmission,
                    Year = car.Year.GetValueOrDefault()
                });
        }
    }
}