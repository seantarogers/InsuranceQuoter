namespace InsuranceQuoter.Presentation.Ui.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public Guid AddressUid { get; set; }

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
    }
}