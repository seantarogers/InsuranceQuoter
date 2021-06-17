namespace InsuranceQuoter.Presentation.Ui.Store.Policy
{
    public record PolicyState
    {
        public string Reference { get; init; }
        public decimal Premium { get; init; }
    }
}