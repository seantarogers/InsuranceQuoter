namespace InsuranceQuoter.Application.Query.Results
{
    using InsuranceQuoter.Message.Dtos;

    public class CarByRegistrationNumberResult : QueryResult
    {
        public CarByRegistrationNumberResult(CarDto car)
        {
            Car = car;
        }

        public CarDto Car { get; }
    }
}