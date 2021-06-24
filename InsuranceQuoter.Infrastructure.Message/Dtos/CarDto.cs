namespace InsuranceQuoter.Infrastructure.Message.Dtos
{
    using Newtonsoft.Json;

    public class CarDto : Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("fueltype")]
        public string FuelType { get; set; }

        [JsonProperty("transmissiontype")]
        public string TransmissionType { get; set; }

        [JsonProperty("yearofmanufacture")]
        public int YearOfManufacture { get; set; }

        [JsonProperty("mileage")]
        public int Mileage { get; set; }
    }
}