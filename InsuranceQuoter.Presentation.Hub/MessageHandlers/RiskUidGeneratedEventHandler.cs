namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class RiskUidGeneratedEventHandler : IHandleMessages<RiskUidGeneratedEvent>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public RiskUidGeneratedEventHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(RiskUidGeneratedEvent message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "RiskUidGeneratedEventHandler",
                message);
    }
}