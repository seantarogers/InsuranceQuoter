namespace InsuranceQuoter.Presentation.Hub.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Presentation.Hub.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class CardPaymentTakenEventHandler : IHandleMessages<CardPaymentTakenEvent>
    {
        private readonly IHubContext<QuoteHub> quoteHubContext;

        public CardPaymentTakenEventHandler(IHubContext<QuoteHub> quoteHubContext)
        {
            this.quoteHubContext = quoteHubContext;
        }

        public Task Handle(CardPaymentTakenEvent message, IMessageHandlerContext context) =>
            quoteHubContext.Clients.All.SendAsync(
                "CardPaymentTakenEventHandler",
                message);
    }
}