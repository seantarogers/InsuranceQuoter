namespace InsuranceQuoter.Presentation.Ui.Store.Car
{
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Models;

    public class CarFeature : Feature<CarState>
    {
        public override string GetName() => nameof(CarState);

        protected override CarState GetInitialState()
        {
            return new CarState
            {
                CarRetrieved = false,
                CarRetrieving = false,
                IsValid = false,
                Model = new CarModel()
            };
        }
    }
}