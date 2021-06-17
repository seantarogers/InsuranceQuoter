namespace InsuranceQuoter.Infrastructure.Contract.Responses
{
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Contract.Dtos;

    public class AddressResponse
    {
        public List<AddressDto> Addresses { get; set; }
    }
}