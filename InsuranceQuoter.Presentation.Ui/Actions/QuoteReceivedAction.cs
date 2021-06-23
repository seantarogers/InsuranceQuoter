namespace InsuranceQuoter.Presentation.Ui.Actions
{
    using System;
    using System.Collections.Generic;

    public record QuoteReceivedAction(Guid Uid, string InsurerName, decimal Premium, List<string> Addons, DateTime StartDate, decimal PremiumTax)
    {
    }
}