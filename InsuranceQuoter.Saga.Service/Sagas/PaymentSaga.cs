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
        public Task Handle(TakePaymentCommand message, IMessageHandlerContext context)
        {
            Data.SagaState = PaymentSagaStates.SagaStarted;
            Data.SagaStateDate = DateTime.UtcNow;
            Data.QuoteReference = message.QuoteReference;

            return context.Send(
                new TakeCardPaymentCommand
                {
                    CardNumber = message.CardNumber,
                    CorrelationId = message.CorrelationId,
                    Amount = message.Amount
                });
        }

        public Task Handle(CardPaymentTakenEvent message, IMessageHandlerContext context)
        {
            Data.SagaState = PaymentSagaStates.PaymentAuthorisedAndTaken;
            Data.SagaStateDate = DateTime.UtcNow;
            Data.PaymentReference = message.PaymentReference;

            return context.Send(
                new BindPolicyWithInsurerCommand
                {
                    CorrelationId = message.CorrelationId,
                    QuoteReference = Data.QuoteReference
                });
        }

        public Task Handle(PolicyBoundEvent message, IMessageHandlerContext context)
        {
            Data.SagaState = PaymentSagaStates.PaymentAuthorisedAndTaken;
            Data.SagaStateDate = DateTime.UtcNow;
            Data.PolicyReference = message.PolicyReference;

            return context.Send(
                new AddPolicyCommand
                {
                    CorrelationId = message.CorrelationId,
                    PaymentReference = Data.PaymentReference,
                    PolicyReference = Data.PolicyReference,
                    QuoteReference = Data.QuoteReference,
                });
        }

        public Task Handle(PolicyAddedEvent message, IMessageHandlerContext context)
        {
            MarkAsComplete();

            return context.Publish(new PurchaseAndBindCompletedEvent());
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PaymentSagaData> mapper)
        {
            mapper.ConfigureMapping<TakePaymentCommand>(m => m.CorrelationId);
            mapper.ConfigureMapping<CardPaymentTakenEvent>(m => m.CorrelationId);
            mapper.ConfigureMapping<PolicyBoundEvent>(m => m.CorrelationId);
            mapper.ConfigureMapping<PolicyAddedEvent>(m => m.CorrelationId);
        }
    }
}