namespace InsuranceQuoter.Presentation.Ui.Actions
{
    using System;

    public record PolicyPurchaseAndBindCompletedAction(string InsurerName, Guid PolicyUid);
}