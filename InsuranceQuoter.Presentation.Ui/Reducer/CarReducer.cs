namespace InsuranceQuoter.Presentation.Ui.Reducer
{
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Car;

    public static class CarReducer
    {
        [ReducerMethod]
        public static CarState Handle(CarState state, FindCarSelectedAction action) =>
            state with
            {
                CarRetrieving = true,
                Model = state.Model,
                CarRetrieved = false
            };

        [ReducerMethod]
        public static CarState Handle(CarState state, CarRetrievedAction action) =>
            state with
            {
                CarRetrieving = false,
                Model = new CarModel
                {
                    Model = action.Model,
                    Make = action.Make,
                    Year = action.Year,
                    Mileage = action.Mileage,
                    Transmission = action.Transmission,
                    Fuel = action.Fuel,
                    Uid = action.Uid,
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
                    Uid = state.Model.Uid,
                    Registration = state.Model.Registration,
                    CoverType = action.CoverType
                }
            };

        [ReducerMethod]
        public static CarState Handle(CarState state, InitializeStateAction action) =>
            new CarState()
            {
                Model = new CarModel()
            };
    }
}