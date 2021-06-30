namespace InsuranceQuoter.Acl.PaymentProvider.Service.MessageHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using NServiceBus;

    public class TakeCardPaymentCommandHandler : IHandleMessages<TakeCardPaymentCommand>
    {
        public Task Handle(TakeCardPaymentCommand message, IMessageHandlerContext context)
        {
            //To simulate latency
            Thread.Sleep(500);

            return context.Publish(
                new CardPaymentTakenEvent
                {
                    CorrelationId = message.CorrelationId,
                    PaymentUid = Guid.NewGuid()
                });
        }
    }
}