namespace InsuranceQuoter.Presentation.Ui.Store.Quotes
{
    using System.Collections.Generic;
    using System.Linq;
    using InsuranceQuoter.Presentation.Ui.Models;

    public record QuoteState
    {
        public List<QuoteModel> Model { get; init; }
        public int TimerTicks{ get; init; }
        public bool QuoteSelected { get; init; }
        public bool ShowPaymentDialog {get; init;}
    }
}