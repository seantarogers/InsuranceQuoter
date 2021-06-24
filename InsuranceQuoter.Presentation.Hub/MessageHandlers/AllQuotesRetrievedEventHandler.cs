namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class AllQuotesRetrievedEventHandler : IHandleMessages<AllQuotesRetrievedEvent>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public AllQuotesRetrievedEventHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(AllQuotesRetrievedEvent message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "AllQuotesRetrievedEventHandler",
                message);
    }
}