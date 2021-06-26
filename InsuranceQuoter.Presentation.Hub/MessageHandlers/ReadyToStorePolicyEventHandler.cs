namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class ReadyToStorePolicyEventHandler : IHandleMessages<ReadyToStorePolicyEvent>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public ReadyToStorePolicyEventHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(ReadyToStorePolicyEvent message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "ReadyToStorePolicyEventHandler",
                message);
    }
}