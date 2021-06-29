namespace InsuranceQuoter.Presentation.Ui.Store.Car
{
    using InsuranceQuoter.Presentation.Ui.Models;

    public record CarState
    {
        public bool CarNotFound { get; init; }
        public bool CarRetrieving { get; init; }
        public bool CarRetrieved { get; init; }
        public bool IsValid { get; init; }
        public CarModel Model { get; init; }
    }
}