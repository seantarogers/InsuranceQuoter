namespace InsuranceQuoter.Presentation.Ui.Reducer
{
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Store.Risk;

    public class RiskReducer
    {
        [ReducerMethod]
        public static RiskState Handle(RiskState state, RiskReferenceGeneratedAction action) =>
            new RiskState()
            {
                RiskReference = action.RiskReference
            };
    }
}