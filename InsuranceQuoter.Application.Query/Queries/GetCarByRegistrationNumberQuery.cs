namespace InsuranceQuoter.Application.Query.Queries
{
    using InsuranceQuoter.Application.Query.Results;

    public class GetCarByRegistrationNumberQuery : Query<CarByRegistrationNumberResult>
    {
        public GetCarByRegistrationNumberQuery(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }

        public string RegistrationNumber { get; }
    }
}