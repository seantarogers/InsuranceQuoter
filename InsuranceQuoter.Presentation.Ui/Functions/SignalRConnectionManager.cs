namespace InsuranceQuoter.Presentation.Ui.Functions
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using Microsoft.AspNetCore.SignalR.Client;

    public class SignalRConnectionManager
    {
        private readonly IDispatcher dispatcher;

        public SignalRConnectionManager(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public static HubConnection HubConnection { get; set; }

        public async Task Initialize()
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:9001/quotehub")
                .Build();

            await HubConnection.StartAsync();

            HubConnection.On(
                "QuoteResponseHandler",
                (QuoteResponse quoteResponse) =>
                {
                    dispatcher.Dispatch(
                        new QuoteReceivedAction(
                            quoteResponse.Uid,
                            quoteResponse.Insurer,
                            quoteResponse.Premium,
                            quoteResponse.Addons,
                            quoteResponse.StartDate,
                            quoteResponse.PremiumTax));
                });

            HubConnection.On(
                "PaymentProviderContactedEventHandler",
                (PaymentProviderContactedEvent _) => { dispatcher.Dispatch(new PaymentProviderContactedAction()); });

            HubConnection.On(
                "CardAuthorisedEventHandler",
                (CardPaymentTakenEvent _) => { dispatcher.Dispatch(new CardAuthorisedAction()); });

            HubConnection.On(
                "PaymentTakenEventHandler",
                (PaymentTakenEvent _) => { dispatcher.Dispatch(new PaymentTakenAction()); });

            HubConnection.On(
                "InsurerContactedEventHandler",
                (InsurerContactedEvent _) => { dispatcher.Dispatch(new InsurerContactedAction()); });

            HubConnection.On(
                "AllQuotesRetrievedEventHandler",
                (AllQuotesRetrievedEvent allQuotesRetrievedEvent) => { dispatcher.Dispatch(new AllQuotesRetrievedAction(allQuotesRetrievedEvent.RiskReference)); });

            HubConnection.On(
                "PolicyBoundEventHandler",
                (PolicyBoundEvent policyBoundEvent) =>
                {
                    dispatcher.Dispatch(new PolicyBoundAction(policyBoundEvent.PolicyReference));

                    Task.Delay(TimeSpan.FromMilliseconds(1000)).ContinueWith(_ => { dispatcher.Dispatch(new PurchaseCompletedAction()); }).GetAwaiter().GetResult();
                });
        }
    }
}