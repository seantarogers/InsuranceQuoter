namespace InsuranceQuoter.Presentation.Ui.Store.Payment
{
    using Fluxor;

    public class PaymentFeature : Feature<PaymentState>
    {
        public override string GetName() => nameof(PaymentState);

        protected override PaymentState GetInitialState() =>
            new PaymentState
            {
            };
    }
}