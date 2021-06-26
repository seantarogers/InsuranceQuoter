namespace InsuranceQuoter.Application.Query.Results
{
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public record AddressesByPostcodeResult(IEnumerable<AddressDto> Addresses) : QueryResult;
}