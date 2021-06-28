namespace InsuranceQuoter.Presentation.Ui.Models
{
    using System;

    public class PolicyModel
    {
        public string Registration { get; set; }
        public string StartsOn { get; set; }
        public string ExpiresOn { get; set; }
        public string CoverType { get; set; }
        public string DriverName { get; set; }
        public string Insurer { get; set; }
        public Guid PolicyUid { get; set; }
    }
}