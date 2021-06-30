namespace InsuranceQuoter.Presentation.Ui.Reducers
{
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Store.Risk;

    public class RiskReducer
    {
        [ReducerMethod]
        public static RiskState Handle(RiskState _, RiskUidGeneratedAction action) =>
            new()
            {
                RiskUid = action.RiskUid
            };
    }
}