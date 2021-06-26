namespace InsuranceQuoter.Presentation.Ui.Effects
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Quotes;
    using InsuranceQuoter.Presentation.Ui.Store.Risk;
    using Microsoft.AspNetCore.SignalR.Client;

    public class PaymentEffects
    {
        private readonly IState<QuoteState> quoteState;
        private readonly IState<RiskState> riskState;

        public PaymentEffects(IState<QuoteState> quoteState, IState<RiskState> riskState)
        {
            this.quoteState = quoteState;
            this.riskState = riskState;
        }

        [EffectMethod]
        public Task Handle(PaymentRequestedAction action, IDispatcher _)
        {
            QuoteModel selectedQuote = quoteState.Value.Model.Single(a => a.Selected);
            Guid riskUid = riskState.Value.RiskUid;

            return SignalRConnectionManager.HubConnection.SendAsync(
                "HandleTakePaymentCommand",
                new TakePaymentCommand
                {
                    RiskUid = riskUid,
                    CardNumber = action.CardNumber,
                    Amount = selectedQuote.Premium,
                    QuoteUid = selectedQuote.Uid,
                    UserName = action.UserName,
                    InsurerName = selectedQuote.Insurer,
                    Addons = selectedQuote.Addons,
                    CorrelationId = Guid.NewGuid(),
                });
        }
    }
}