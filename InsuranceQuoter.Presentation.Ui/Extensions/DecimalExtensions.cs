namespace InsuranceQuoter.Presentation.Ui.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToPounds(this decimal amount) =>
            amount.ToString("C").Replace("GBP", "£");
    }
}