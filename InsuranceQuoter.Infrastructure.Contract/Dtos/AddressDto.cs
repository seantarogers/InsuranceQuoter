namespace InsuranceQuoter.Infrastructure.Contract.Dtos
{
    using System;

    public class AddressDto
    {
        public Guid Uid { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}