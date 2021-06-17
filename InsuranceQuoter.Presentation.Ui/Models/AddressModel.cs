namespace InsuranceQuoter.Presentation.Ui.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddressModel
    {
        [Required]
        public Guid Uid { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        [Required]
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string Postcode { get; set; }

        public override string ToString() =>
            AddressLine1 + ", " + AddressLine2 + ", " + City + ", " + County + ", " + Postcode;
    }
}