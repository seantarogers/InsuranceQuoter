namespace InsuranceQuoter.Presentation.Hub.Hubs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class QuoteHub : Hub
    {
        private readonly IMessageSession messageSession;

        public QuoteHub(IMessageSession messageSession)
        {
            this.messageSession = messageSession;
        }

        public async Task HandleTakePaymentCommandAsync(TakePaymentCommand command)
        {
            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "PaymentProviderContactedEventHandler",
                new PaymentProviderContactedEvent()
            );

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "CardAuthorisedEventHandler",
                new CardPaymentTakenEvent()
                {
                    PaymentReference = ""
                });

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "PaymentTakenEventHandler",
                new PaymentTakenEvent
                {
                    CardNumber = command.CardNumber,
                    PaymentUid = Guid.NewGuid()
                });

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "InsurerContactedEventHandler",
                new InsurerContactedEvent());

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "PolicyBoundEventHandler",
                new PolicyBoundEvent
                {
                    PolicyReference = Guid.NewGuid().ToString()
                });
        }

        public async Task HandleQuotesRequestAsync(QuoteRequest request)
        {
            await messageSession.Send(
                new AbcInsurerQuoteRequest
                {
                    CarId = request.CarId,
                    Make = request.Make,
                    Model = request.Model,
                    Mileage = request.Mileage,
                    Fuel = request.Fuel,
                    Transmission = request.Transmission,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    AddressUid = request.AddressUid,
                    City = request.City,
                    County = request.County,
                    CoverType = request.CoverType,
                    DateOfBirth = request.DateOfBirth,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Postcode = request.Postcode,
                    Registration = request.Registration,
                    Year = request.Year
                });

            await messageSession.Send(
                new DefInsurerQuoteRequest()
                {
                    CarId = request.CarId,
                    Make = request.Make,
                    Model = request.Model,
                    Mileage = request.Mileage,
                    Fuel = request.Fuel,
                    Transmission = request.Transmission,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    AddressUid = request.AddressUid,
                    City = request.City,
                    County = request.County,
                    CoverType = request.CoverType,
                    DateOfBirth = request.DateOfBirth,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Postcode = request.Postcode,
                    Registration = request.Registration,
                    Year = request.Year
                });

            await messageSession.Send(
                new GhiInsurerQuoteRequest()
                {
                    CarId = request.CarId,
                    Make = request.Make,
                    Model = request.Model,
                    Mileage = request.Mileage,
                    Fuel = request.Fuel,
                    Transmission = request.Transmission,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    AddressUid = request.AddressUid,
                    City = request.City,
                    County = request.County,
                    CoverType = request.CoverType,
                    DateOfBirth = request.DateOfBirth,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Postcode = request.Postcode,
                    Registration = request.Registration,
                    Year = request.Year
                });

            await messageSession.Send(
                new JklInsurerQuoteRequest()
                {
                    CarId = request.CarId,
                    Make = request.Make,
                    Model = request.Model,
                    Mileage = request.Mileage,
                    Fuel = request.Fuel,
                    Transmission = request.Transmission,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    AddressUid = request.AddressUid,
                    City = request.City,
                    County = request.County,
                    CoverType = request.CoverType,
                    DateOfBirth = request.DateOfBirth,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Postcode = request.Postcode,
                    Registration = request.Registration,
                    Year = request.Year
                });

            await messageSession.Send(
                new MnoInsurerQuoteRequest()
                {
                    CarId = request.CarId,
                    Make = request.Make,
                    Model = request.Model,
                    Mileage = request.Mileage,
                    Fuel = request.Fuel,
                    Transmission = request.Transmission,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    AddressUid = request.AddressUid,
                    City = request.City,
                    County = request.County,
                    CoverType = request.CoverType,
                    DateOfBirth = request.DateOfBirth,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Postcode = request.Postcode,
                    Registration = request.Registration,
                    Year = request.Year
                });
        }
    }
}