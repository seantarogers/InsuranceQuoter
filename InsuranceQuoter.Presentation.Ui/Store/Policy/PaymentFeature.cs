namespace InsuranceQuoter.Presentation.Ui.Store.Policy
{
    using Fluxor;

    public class PolicyFeature : Feature<PolicyState>
    {
        public override string GetName() => nameof(PolicyState);

        protected override PolicyState GetInitialState() =>
            new PolicyState
            {
            };
    }
}