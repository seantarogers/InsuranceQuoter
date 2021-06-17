namespace InsuranceQuoter.Presentation.Ui.Actions
{
    using System;
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public record QuoteReceivedAction(Guid Uid, string InsurerName, decimal Premium, List<string> Addons, DateTime StartDate, decimal PremiumTax)
    {
    }
}