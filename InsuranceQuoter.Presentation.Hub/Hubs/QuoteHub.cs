namespace InsuranceQuoter.Presentation.Hub.Hubs
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class QuoteHub : Hub
    {
        private readonly IMessageSession messageSession;

        public QuoteHub(IMessageSession messageSession)
        {
            this.messageSession = messageSession;
        }

        public async Task HandleTakePaymentCommand(TakePaymentCommand command)
        {
            await messageSession.Send(command).ConfigureAwait(false);
        }

        public async Task HandleQuotesRequest(QuoteRequest request)
        {
            await messageSession.Send(request).ConfigureAwait(false);
        }
    }
}