namespace InsuranceQuoter.Application.Query.Results
{
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class CarByRegistrationNumberResult : QueryResult
    {
        public CarByRegistrationNumberResult(CarDto car)
        {
            Car = car;
        }

        public CarDto Car { get; }
    }
}