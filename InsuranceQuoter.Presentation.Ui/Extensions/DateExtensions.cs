namespace InsuranceQuoter.Presentation.Ui.Extensions
{
    using System;

    public static class DateExtensions
    {
        public static string ToNiceDate(this DateTime dateTime) =>
            dateTime.ToString("dddd, dd MMMM yyyy");
    }
}