namespace InsuranceQuoter.Infrastructure.Message.Commands
{
    using System;

    public class AddRiskCommand : Message
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid AddressUid { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Fuel { get; set; }
        public string Transmission { get; set; }
        public string Registration { get; set; }
        public string CoverType { get; set; }
    }
}