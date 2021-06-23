namespace InsuranceQuoter.Infrastructure.Message.Responses
{
    using System.Collections.Generic;
    using InsuranceQuoter.Message.Dtos;

    public class AddressResponse
    {
        public List<AddressDto> Addresses { get; set; }
    }
}