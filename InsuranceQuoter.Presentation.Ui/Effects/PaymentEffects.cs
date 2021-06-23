namespace InsuranceQuoter.Presentation.Ui.Effects
{
    using System.Linq;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Quotes;
    using Microsoft.AspNetCore.SignalR.Client;

    public class PaymentEffects
    {
        private readonly IState<QuoteState> quoteState;

        public PaymentEffects(IState<QuoteState> quoteState)
        {
            this.quoteState = quoteState;
        }

        [EffectMethod]
        public Task Handle(PaymentRequestedAction action, IDispatcher dispatcher)
        {
            QuoteModel selectedQuote = quoteState.Value.Model.Single(a => a.Selected);

            return SignalRConnectionManager.HubConnection.SendAsync(
                "HandleTakePaymentCommandAsync",
                new TakePaymentCommand()
                {
                    CardNumber = action.CardNumber,
                    Amount = selectedQuote.Premium,
                    QuoteReference = "Payment for Car Insurance"
                });
        }
    }
}