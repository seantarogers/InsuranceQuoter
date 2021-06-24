namespace InsuranceQuoter.Presentation.Ui.Store.Risk
{
    using System;

    public record RiskState
    {
        public Guid RiskReference { get; init; }
    }
}