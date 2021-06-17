namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Payment;
    using InsuranceQuoter.Presentation.Ui.Store.Quotes;
    using Microsoft.AspNetCore.Components;

    public partial class Quotes
    {
        [Inject]
        public IState<QuoteState> QuoteState { get; set; }

        [Inject]
        public IState<PaymentState> PaymentState { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public QuoteModel SelectedQuote => QuoteState.Value.Model.SingleOrDefault(a => a.Selected);
        public bool ShowPaymentDialog => QuoteState.Value.ShowPaymentDialog;
        public bool QuoteHasBeenSelected => QuoteState.Value.QuoteSelected;
        public List<QuoteModel> QuoteModel => QuoteState.Value.Model;
        public PaymentModel PaymentModel => PaymentState.Value.Model;
        public int TimerTicks => QuoteState.Value.TimerTicks;
        public string TimerPercentage => QuoteState.Value.TimerTicks * 10 + "%";

        public void QuoteSelected(Guid uid)
        {
            Dispatcher.Dispatch(new QuotesSelectedAction(uid));
        }

        public void CloseDialogSelected()
        {
            Guid selectedQuoteUid = SelectedQuote.Uid;

            Dispatcher.Dispatch(new CloseDialogSelectedAction(selectedQuoteUid));
        }

        public void QuoteBackSelected()
        {
            Dispatcher.Dispatch(new QuotesBackSelectedAction());

            NavigationManager.NavigateTo("car");
        }

        private void HandleValidSubmit()
        {
            NavigationManager.NavigateTo("paymentprogress");

            Dispatcher.Dispatch(
                new PaymentRequestedAction(PaymentModel.CardNumber));
        }
    }
}