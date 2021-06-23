namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class QuoteResponseHandler : IHandleMessages<QuoteResponse>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public QuoteResponseHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(QuoteResponse message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "QuoteResponseHandler",
                message);
    }
}