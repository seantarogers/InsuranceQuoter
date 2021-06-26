namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class PolicyBoundEventHandler : IHandleMessages<PolicyBoundEvent>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public PolicyBoundEventHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(PolicyBoundEvent message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "PolicyBoundEventHandler",
                message);
    }
}