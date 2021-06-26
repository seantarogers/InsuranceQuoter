namespace InsuranceQuoter.Application.Query.Queries
{
    using InsuranceQuoter.Application.Query.Results;

    public record GetCarByRegistrationNumberQuery(string RegistrationNumber) : Query<CarByRegistrationNumberResult>;
}