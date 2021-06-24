namespace InsuranceQuoter.Application.Query.Results
{
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class AddressesByPostcodeResult : QueryResult
    {
        public AddressesByPostcodeResult(IEnumerable<AddressDto> addresses)
        {
            Addresses = addresses;
        }

        public IEnumerable<AddressDto> Addresses { get; }
    }
}