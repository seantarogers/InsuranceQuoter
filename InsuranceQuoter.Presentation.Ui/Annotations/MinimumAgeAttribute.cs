namespace InsuranceQuoter.Presentation.Ui.Annotations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MinimumAgeAttribute : ValidationAttribute
    {
        public MinimumAgeAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime date))
            {
                return date.AddYears(18) < DateTime.Now;
            }

            return false;
        }
    }
}