namespace InsuranceQuoter.Presentation.Ui.Models
{
    using System;

    public class CarModel
    {
        public Guid Uid { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public int? Mileage { get; set; }
        public string Fuel { get; set; }
        public string Transmission { get; set; }
        public string Registration { get; set; }
        public string CoverType { get; set; }
    }
}