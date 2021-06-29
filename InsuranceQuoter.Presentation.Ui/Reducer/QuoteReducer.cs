namespace InsuranceQuoter.Presentation.Ui.Reducer
{
    using System.Collections.Generic;
    using System.Linq;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Quotes;

    public static class QuoteReducer
    {
        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, SortQuotesAscendingByPremiumRequestedAction _) =>
            state with
            {
                Model = state.Model.OrderBy(a => a.Premium).ToList()
            };

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, SortQuotesDescendingByPremiumRequestedAction _) =>
            state with
            {
                Model = state.Model.OrderByDescending(a => a.Premium).ToList()
            };

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, SortQuotesAscendingByInsurerRequestedAction _) =>
            state with
            {
                Model = state.Model.OrderBy(a => a.Premium).ToList()
            };

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, SortQuotesDescendingByInsurerRequestedAction _) =>
            state with
            {
                Model = state.Model.OrderByDescending(a => a.Premium).ToList()
            };

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, QuoteReceivedAction action)
        {
            var quotes = new List<QuoteModel>();

            foreach (QuoteModel existingQuote in state.Model)
            {
                quotes.Add(
                    new QuoteModel
                    {
                        Uid = existingQuote.Uid,
                        Addons = existingQuote.Addons,
                        Insurer = existingQuote.Insurer,
                        Premium = existingQuote.Premium,
                        StartDate = existingQuote.StartDate,
                        PremiumTax = existingQuote.PremiumTax,
                        Selected = false,
                        SelectedClass = string.Empty
                    });
            }

            quotes.Add(
                new QuoteModel
                {
                    Uid = action.Uid,
                    Insurer = action.InsurerName,
                    Addons = string.Join(", ", action.Addons),
                    StartDate = action.StartDate.ToString("dddd, dd MMMM yyyy"),
                    Premium = action.Premium,
                    PremiumTax = action.PremiumTax,
                    Selected = false,
                    SelectedClass = string.Empty
                });

            return new QuoteState()
            {
                Model = quotes,
                TimerTicks = state.TimerTicks,
                QuoteSelected = false,
            };
        }

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, QuotesSelectedAction action)
        {
            state.Model.ForEach(a => a.Selected = false);
            state.Model.ForEach(a => a.SelectedClass = string.Empty);

            QuoteModel selectedQuote = state.Model.Single(a => a.Uid == action.Uid);

            selectedQuote.Selected = true;
            selectedQuote.SelectedClass = "yello";

            return new QuoteState
            {
                QuoteSelected = true,
                Model = state.Model.Select(
                    a => new QuoteModel()
                    {
                        Addons = a.Addons,
                        Insurer = a.Insurer,
                        Premium = a.Premium,
                        PremiumTax = a.PremiumTax,
                        Selected = a.Selected,
                        StartDate = a.StartDate,
                        SelectedClass = a.SelectedClass,
                        Uid = a.Uid
                    }).ToList(),
                TimerTicks = state.TimerTicks,
                ShowPaymentDialog = true
            };
        }

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, QuotesBackSelectedAction action) =>
            new()
            {
                QuoteSelected = false,
                TimerTicks = 0,
                Model = new List<QuoteModel>()
            };

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, TimerElapsedAction action) =>
            state with
            {
                TimerTicks = state.TimerTicks + 1
            };

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, TimerFinishedAction action) =>
            state with
            {
                TimerTicks = 0
            };

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, CloseDialogSelectedAction action)
        {
            return new()
            {
                QuoteSelected = false,
                Model = state.Model.Select(
                    a => new QuoteModel()
                    {
                        Addons = a.Addons,
                        Insurer = a.Insurer,
                        Premium = a.Premium,
                        PremiumTax = a.PremiumTax,
                        Selected = false,
                        StartDate = a.StartDate,
                        Uid = a.Uid,
                        SelectedClass = string.Empty
                    }).ToList(),
                TimerTicks = state.TimerTicks,
                ShowPaymentDialog = false
            };
        }

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, PurchaseOperationCompletedAction action) =>
            state with
            {
                ShowPaymentDialog = false
            };

        [ReducerMethod]
        public static QuoteState Handle(QuoteState state, InitializeStateAction action) =>
            new()
            {
                Model = new List<QuoteModel>(),
                QuoteSelected = false,
                ShowPaymentDialog = false,
                TimerTicks = 0
            };
    }
}