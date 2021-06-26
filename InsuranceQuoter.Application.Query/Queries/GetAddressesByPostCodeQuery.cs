namespace InsuranceQuoter.Application.Query.Queries
{
    using InsuranceQuoter.Application.Query.Results;

    public record GetAddressesByPostCodeQuery(string Postcode) : Query<AddressesByPostcodeResult>;
}