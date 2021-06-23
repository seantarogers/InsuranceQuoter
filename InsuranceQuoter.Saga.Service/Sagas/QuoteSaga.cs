namespace InsuranceQuoter.Saga.Service.Sagas
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Saga.Service.SagaData;
    using NServiceBus;

    public class QuoteSaga : Saga<QuoteSagaData>,
        IAmStartedByMessages<RetrieveQuotesCommand>,
        IHandleMessages<RiskAddedEvent>,
        IHandleMessages<QuoteResponse>
    {
        public Task Handle(RetrieveQuotesCommand message, IMessageHandlerContext context) =>
            //Data.NumberOfInsurers = 5;
            throw new NotImplementedException();

        public Task Handle(QuoteResponse message, IMessageHandlerContext context) =>
            throw new NotImplementedException();

        public Task Handle(RiskAddedEvent message, IMessageHandlerContext context) =>
            throw new NotImplementedException();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<QuoteSagaData> mapper)
        {
            throw new NotImplementedException();
        }
    }
}