namespace InsuranceQuoter.Presentation.Ui.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PaymentModel
    {
        [CreditCard]
        public string CardNumber { get; set; }

        public bool TermsAndConditionsChecked { get; set; }
    }
}