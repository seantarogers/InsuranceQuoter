namespace InsuranceQuoter.Application.Query.Queries
{
    using InsuranceQuoter.Application.Query.Results;

    public class GetAddressesByPostCodeQuery : Query<AddressesByPostcodeResult>
    {
        public GetAddressesByPostCodeQuery(string postCode)
        {
            PostCode = postCode;
        }

        public string PostCode { get; }
    }
}