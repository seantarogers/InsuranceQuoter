namespace InsuranceQuoter.Presentation.Ui.Store.Risk
{
    using Fluxor;

    public class RiskFeature : Feature<RiskState>
    {
        public override string GetName() => nameof(RiskState);

        protected override RiskState GetInitialState() =>
            new();
    }
}