﻿namespace InsuranceQuoter.Infrastructure.Message.Requests
{
    using System;

    public class QuotesRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid AddressUid { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public Guid CarUid { get; set; }
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