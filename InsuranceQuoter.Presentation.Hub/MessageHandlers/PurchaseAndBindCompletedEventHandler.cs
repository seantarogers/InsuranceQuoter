namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class PurchaseAndBindCompletedEventHandler : IHandleMessages<PurchaseAndBindCompletedEvent>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public PurchaseAndBindCompletedEventHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(PurchaseAndBindCompletedEvent message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "PurchaseAndBindCompletedEventHandler",
                message);
    }
}