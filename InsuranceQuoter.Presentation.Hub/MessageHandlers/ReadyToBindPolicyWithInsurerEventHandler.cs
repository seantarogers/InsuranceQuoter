namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class ReadyToBindPolicyWithInsurerEventHandler : IHandleMessages<ReadyToBindPolicyWithInsurerEvent>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public ReadyToBindPolicyWithInsurerEventHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(ReadyToBindPolicyWithInsurerEvent message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "ReadyToBindPolicyWithInsurerEventHandler",
                message);
    }
}