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
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);

            return context.Publish(
                new CardPaymentTakenEvent
                {
                    CorrelationId = message.CorrelationId,
                    PaymentUid = Guid.NewGuid()
                });
        }
    }
}