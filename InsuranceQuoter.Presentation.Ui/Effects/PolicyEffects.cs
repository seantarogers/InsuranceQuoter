namespace InsuranceQuoter.Presentation.Ui.Effects
{
    using System.Linq;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Payment;
    using InsuranceQuoter.Presentation.Ui.Store.Quotes;

    public class PolicyEffects
    {
        //private readonly IDispatcher dispatcher;
        //private readonly IState<QuoteState> quoteState;
        //private readonly IState<PaymentState> paymentState;

        //public PolicyEffects(IState<QuoteState> quoteState, IState<PaymentState> paymentState, IDispatcher dispatcher)
        //{
        //    this.quoteState = quoteState;
        //    this.paymentState = paymentState;
        //    this.dispatcher = dispatcher;
        //}

        //[EffectMethod]
        //public Task Handle(PaymentRequestedAction action, IDispatcher dispatcher)
        //{
        //    QuoteModel selectedQuote = quoteState.Value.Model.Single(a => a.Selected);
        //    string policyReference = paymentState.Value.PolicyReference;

        //    dispatcher.Dispatch(new PolicyPurchasedAction(policyReference, selectedQuote.Premium));

        //    return Task.CompletedTask;
        //}
    }
}