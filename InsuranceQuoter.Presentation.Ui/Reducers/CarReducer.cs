namespace InsuranceQuoter.Presentation.Ui.Reducers
{
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Car;

    public static class CarReducer
    {
        [ReducerMethod]
        public static CarState Handle(CarState state, CarNotFoundAction _) =>
            state with
            {
                CarNotFound = true,
                CarRetrieving = false,
                Model = new CarModel(),
                CarRetrieved = true
            };

        [ReducerMethod]
        public static CarState Handle(CarState state, FindCarSelectedAction _) =>
            state with
            {
                CarNotFound = false,
                CarRetrieving = true,
                Model = state.Model,
                CarRetrieved = false
            };

        [ReducerMethod]
        public static CarState Handle(CarState state, CarRetrievedAction action) =>
            state with
            {
                CarNotFound = false,
                CarRetrieving = false,
                Model = new CarModel
                {
                    Model = action.Model,
                    Make = action.Make,
                    Year = action.Year,
                    Mileage = action.Mileage,
                    Transmission = action.Transmission,
                    Fuel = action.Fuel,
                    Id = action.Id,
                    Registration = action.Registration,
                    CoverType = state.Model.CoverType
                },
                CarRetrieved = true
            };

        [ReducerMethod]
        public static CarState Handle(CarState state, CoverTypeSelectionAction action) =>
            state with
            {
                Model = new CarModel
                {
                    Model = state.Model.Model,
                    Make = state.Model.Make,
                    Year = state.Model.Year,
                    Mileage = state.Model.Mileage,
                    Transmission = state.Model.Transmission,
                    Fuel = state.Model.Fuel,
                    Id = state.Model.Id,
                    Registration = state.Model.Registration,
                    CoverType = action.CoverType
                }
            };

        [ReducerMethod]
        public static CarState Handle(CarState _, InitializeStateAction __) =>
            new()
            {
                Model = new CarModel()
            };
    }
}