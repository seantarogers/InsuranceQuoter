namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class ReadyToContactPaymentProviderEventHandler : IHandleMessages<ReadyToContactPaymentProviderEvent>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public ReadyToContactPaymentProviderEventHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(ReadyToContactPaymentProviderEvent message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "ReadyToContactPaymentProviderEventHandler",
                message);
    }
}