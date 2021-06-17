namespace InsuranceQuoter.Presentation.Ui.Store.Quotes
{
    using System.Collections.Generic;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Car;

    public class QuoteFeature : Feature<QuoteState>
    {
        public override string GetName() => nameof(QuoteState);

        protected override QuoteState GetInitialState()
        {
            return new QuoteState
            {
                Model = new List<QuoteModel>()
            };
        }
    }
}