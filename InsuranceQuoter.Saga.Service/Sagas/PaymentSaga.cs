namespace InsuranceQuoter.Saga.Service.Sagas
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Saga.Service.SagaData;
    using InsuranceQuoter.Saga.Service.SagaStates;
    using NServiceBus;

    public class PaymentSaga : Saga<PaymentSagaData>,
        IAmStartedByMessages<TakePaymentCommand>,
        IHandleMessages<CardPaymentTakenEvent>,
        IHandleMessages<PolicyBoundEvent>,
        IHandleMessages<PolicyAddedEvent>
    {
        public async Task Handle(TakePaymentCommand message, IMessageHandlerContext context)
        {
            Data.SagaState = PaymentSagaStates.SagaStarted;
            Data.SagaStateDate = DateTime.UtcNow;
            Data.QuoteUid = message.QuoteUid;
            Data.UserName = message.UserName;
            Data.InsurerName = message.InsurerName;
            Data.Addons = message.Addons;
            Data.Amount = message.Amount;
            Data.RiskUid = message.RiskUid;

            await context.Publish(
                new ReadyToContactPaymentProviderEvent
                {
                    CorrelationId = message.CorrelationId
                }).ConfigureAwait(false);

            await context.Send(
                new TakeCardPaymentCommand
                {
                    CardNumber = message.CardNumber,
                    CorrelationId = message.CorrelationId,
                    Amount = message.Amount
                });
        }

        public async Task Handle(CardPaymentTakenEvent message, IMessageHandlerContext context)
        {
            Data.SagaState = PaymentSagaStates.PaymentAuthorisedAndTaken;
            Data.SagaStateDate = DateTime.UtcNow;
            Data.PaymentUid = message.PaymentUid;

            await context.Publish(
                new ReadyToBindPolicyWithInsurerEvent
                {
                    CorrelationId = message.CorrelationId
                }).ConfigureAwait(false);

            await context.Send(
                new BindPolicyWithInsurerCommand
                {
                    CorrelationId = message.CorrelationId,
                    QuoteUid = Data.QuoteUid,
                    InsurerName = Data.InsurerName
                });
        }

        public async Task Handle(PolicyBoundEvent message, IMessageHandlerContext context)
        {
            Data.SagaState = PaymentSagaStates.PaymentAuthorisedAndTaken;
            Data.SagaStateDate = DateTime.UtcNow;
            Data.PolicyUid = message.PolicyUid;

            await context.Publish(
                new ReadyToStorePolicyEvent
                {
                    CorrelationId = message.CorrelationId
                }).ConfigureAwait(false);

            await context.Send(
                new AddPolicyCommand
                {
                    CorrelationId = message.CorrelationId,
                    PolicyUid = Data.PolicyUid,
                    RiskUid = Data.RiskUid,
                    QuoteUid = Data.QuoteUid,
                    PaymentUid = Data.PaymentUid,
                    InsurerName = Data.InsurerName,
                    Username = Data.UserName,
                    Addons = Data.Addons
                });
        }

        public Task Handle(PolicyAddedEvent message, IMessageHandlerContext context)
        {
            MarkAsComplete();

            return context.Publish(
                new PurchaseAndBindCompletedEvent
                {
                    InsurerName = Data.InsurerName,
                    CorrelationId = message.CorrelationId,
                    PolicyUid = Data.PolicyUid
                });
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PaymentSagaData> mapper)
        {
            mapper.ConfigureMapping<TakePaymentCommand>(m => m.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);

            mapper.ConfigureMapping<CardPaymentTakenEvent>(m => m.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);

            mapper.ConfigureMapping<PolicyBoundEvent>(m => m.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);

            mapper.ConfigureMapping<PolicyAddedEvent>(m => m.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);
        }
    }
}