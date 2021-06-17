namespace InsuranceQuoter.Presentation.Ui.Models
{
    using System;

    public class QuoteModel
    {
        public Guid Uid { get; set; }
        public string Insurer { get; set; }
        public decimal Premium { get; set; }
        public decimal PremiumTax { get; set; }
        public DateTime StartDate { get; set; }
        public string Addons { get; set; }
        public bool Selected { get; set; }
        public string SelectedClass { get; set; }
        public string CardNumber { get; set; }
    }
}