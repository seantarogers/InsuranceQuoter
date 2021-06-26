namespace InsuranceQuoter.Saga.Service.Sagas
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Saga.Service.SagaData;
    using InsuranceQuoter.Saga.Service.SagaStates;
    using NServiceBus;

    public class QuoteSaga : Saga<QuoteSagaData>,
        IAmStartedByMessages<QuoteRequest>,
        IHandleMessages<RiskAddedEvent>,
        IHandleMessages<QuoteResponse>
    {
        public async Task Handle(QuoteRequest message, IMessageHandlerContext context)
        {
            Data.SagaState = QuoteSagaStates.SagaStarted;
            Data.CorrelationId = message.CorrelationId;
            Data.SagaStateDate = DateTime.UtcNow;
            Data.NumberOfInsurerRequestsSent = 5;
            Data.QuoteRequest = message;

            AddRiskCommand addRiskCommand = BuildAddRiskCommand(message);
            await context.Send(addRiskCommand).ConfigureAwait(false);

            var abcInsurerQuoteRequest = BuildInsurerSpecificRequest<AbcInsurerQuoteRequest>(Data.QuoteRequest, message.CorrelationId);
            var defInsurerQuoteRequest = BuildInsurerSpecificRequest<DefInsurerQuoteRequest>(Data.QuoteRequest, message.CorrelationId);
            var ghiInsurerQuoteRequest = BuildInsurerSpecificRequest<GhiInsurerQuoteRequest>(Data.QuoteRequest, message.CorrelationId);
            var jklInsurerQuoteRequest = BuildInsurerSpecificRequest<JklInsurerQuoteRequest>(Data.QuoteRequest, message.CorrelationId);
            var mnoInsurerQuoteRequest = BuildInsurerSpecificRequest<MnoInsurerQuoteRequest>(Data.QuoteRequest, message.CorrelationId);

            await context.Send(abcInsurerQuoteRequest).ConfigureAwait(false);
            await context.Send(defInsurerQuoteRequest).ConfigureAwait(false);
            await context.Send(ghiInsurerQuoteRequest).ConfigureAwait(false);
            await context.Send(jklInsurerQuoteRequest).ConfigureAwait(false);
            await context.Send(mnoInsurerQuoteRequest).ConfigureAwait(false);
        }

        public Task Handle(RiskAddedEvent message, IMessageHandlerContext context)
        {
            Data.SagaState = QuoteSagaStates.RiskAdded;
            Data.SagaStateDate = DateTime.UtcNow;
            Data.RiskAdded = true;
            Data.RiskUid = message.RiskUid;

            if (Data.NumberOfInsurerRequestsReceived == Data.NumberOfInsurerRequestsSent)
            {
                MarkAsComplete();
            }

            return context.Publish(
                new RiskUidGeneratedEvent
                {
                    RiskUid = Data.RiskUid,
                    CorrelationId = message.CorrelationId
                });
        }

        public Task Handle(QuoteResponse message, IMessageHandlerContext context)
        {
            Data.SagaStateDate = DateTime.UtcNow;
            Data.NumberOfInsurerRequestsReceived += 1;
            Data.SagaState = string.Format(QuoteSagaStates.QuoteReceived, Data.NumberOfInsurerRequestsReceived, Data.NumberOfInsurerRequestsSent);

            if (Data.NumberOfInsurerRequestsReceived == Data.NumberOfInsurerRequestsSent && Data.RiskAdded)
            {
                MarkAsComplete();
            }

            return ReplyToOriginator(context, message);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<QuoteSagaData> mapper)
        {
            mapper.ConfigureMapping<QuoteRequest>(m => m.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);
            mapper.ConfigureMapping<QuoteResponse>(m => m.CorrelationId).ToSaga(sagaData => sagaData.CorrelationId);
            mapper.ConfigureMapping<RiskAddedEvent>(m => m.CorrelationId).ToSaga(sagaData => sagaData.CorrelationId);
        }

        private static AddRiskCommand BuildAddRiskCommand(QuoteRequest message) =>
            new()
            {
                UserName = message.UserName,
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
                Year = message.Year,
                CorrelationId = message.CorrelationId
            };

        private static TRequest BuildInsurerSpecificRequest<TRequest>(QuoteRequest message, Guid correlationId)
            where TRequest : QuoteRequest, new() =>
            new()
            {
                CorrelationId = correlationId,
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
            };
    }
}