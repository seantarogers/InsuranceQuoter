namespace InsuranceQuoter.Infrastructure.Contract.Responses
{
    using System;

    public class CarResponse
    {
        public Guid Uid { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Fuel { get; set; }
        public string Transmission { get; set; }
    }
}