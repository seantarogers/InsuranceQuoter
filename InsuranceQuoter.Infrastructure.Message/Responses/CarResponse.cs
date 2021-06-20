namespace InsuranceQuoter.Infrastructure.Message.Responses
{
    public class CarResponse
    {
        public string Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Fuel { get; set; }
        public string Transmission { get; set; }
    }
}