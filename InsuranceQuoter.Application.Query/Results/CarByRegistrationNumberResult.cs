namespace InsuranceQuoter.Application.Query.Results
{
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public record CarByRegistrationNumberResult(CarDto CarDto) : QueryResult;
}