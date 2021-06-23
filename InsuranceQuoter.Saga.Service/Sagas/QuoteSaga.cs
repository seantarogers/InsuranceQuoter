namespace InsuranceQuoter.Saga.Service.Sagas
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Saga.Service.SagaData;
    using NServiceBus;

    public class QuoteSaga : Saga<QuoteSagaData>,
        IAmStartedByMessages<QuoteRequest>,
        IHandleMessages<AbcInsurerQuotesResponse>,
        IHandleMessages<DefInsurerQuotesResponse>,
        IHandleMessages<GhiInsurerQuotesResponse>,
        IHandleMessages<JklInsuranceQuoteResponse>,
        IHandleMessages<MnoInsuranceQuoteResponse>
    {
        public async Task Handle(QuoteRequest message, IMessageHandlerContext context)
        {
            Data.RequestsSent = 5;

            await context.Send(
                new AbcInsurerQuoteRequest()
                {
                    CarId = message.CarId,
                    Make = message.Make,
                    Model = message.Model,
                    Mileage = message.Mileage,
                    Fuel = message.Fuel,
                    Transmission = message.Transmission,
                    AddressLine1 = message.AddressLine1,
                    AddressLine2 = message.AddressLine2,
                    AddressUid = message.AddressUid,
                    City = message.City,
                    County = message.County,
                    CoverType = message.CoverType,
                    DateOfBirth = message.DateOfBirth,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    Postcode = message.Postcode,
                    Registration = message.Registration,
                    Year = message.Year
                });

            await context.Send(
                new DefInsurerQuoteRequest()
                {
                    CarId = message.CarId,
                    Make = message.Make,
                    Model = message.Model,
                    Mileage = message.Mileage,
                    Fuel = message.Fuel,
                    Transmission = message.Transmission,
                    AddressLine1 = message.AddressLine1,
                    AddressLine2 = message.AddressLine2,
                    AddressUid = message.AddressUid,
                    City = message.City,
                    County = message.County,
                    CoverType = message.CoverType,
                    DateOfBirth = message.DateOfBirth,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    Postcode = message.Postcode,
                    Registration = message.Registration,
                    Year = message.Year
                });

            await context.Send(
                new JklInsurerQuoteRequest()
                {
                    CarId = message.CarId,
                    Make = message.Make,
                    Model = message.Model,
                    Mileage = message.Mileage,
                    Fuel = message.Fuel,
                    Transmission = message.Transmission,
                    AddressLine1 = message.AddressLine1,
                    AddressLine2 = message.AddressLine2,
                    AddressUid = message.AddressUid,
                    City = message.City,
                    County = message.County,
                    CoverType = message.CoverType,
                    DateOfBirth = message.DateOfBirth,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    Postcode = message.Postcode,
                    Registration = message.Registration,
                    Year = message.Year
                });

            await context.Send(
                new MnoInsurerQuoteRequest()
                {
                    CarId = message.CarId,
                    Make = message.Make,
                    Model = message.Model,
                    Mileage = message.Mileage,
                    Fuel = message.Fuel,
                    Transmission = message.Transmission,
                    AddressLine1 = message.AddressLine1,
                    AddressLine2 = message.AddressLine2,
                    AddressUid = message.AddressUid,
                    City = message.City,
                    County = message.County,
                    CoverType = message.CoverType,
                    DateOfBirth = message.DateOfBirth,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    Postcode = message.Postcode,
                    Registration = message.Registration,
                    Year = message.Year
                });
        }

        public Task Handle(AbcInsurerQuotesResponse message, IMessageHandlerContext context)
        {
            Data.RequestsSent++;

            //ReplyToOriginator(new QuoteResponse()
            //{

            //})

            throw new NotImplementedException();
        }

        public Task Handle(DefInsurerQuotesResponse message, IMessageHandlerContext context) =>
            throw new NotImplementedException();

        public Task Handle(GhiInsurerQuotesResponse message, IMessageHandlerContext context) =>
            throw new NotImplementedException();

        public Task Handle(JklInsuranceQuoteResponse message, IMessageHandlerContext context) =>
            throw new NotImplementedException();

        public Task Handle(MnoInsuranceQuoteResponse message, IMessageHandlerContext context) =>
            throw new NotImplementedException();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<QuoteSagaData> mapper)
        {
        }
    }
}