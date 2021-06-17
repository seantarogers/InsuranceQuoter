namespace InsuranceQuoter.Presentation.Ui.Store.Payment
{
    using System.Collections.Generic;
    using InsuranceQuoter.Presentation.Ui.Models;

    public record PaymentState
    {
        public string PolicyReference { get; set; }
        public bool PaymentProcessing { get; init; }
        public bool PaymentProcessed { get; init; }
        public PaymentModel Model { get; init; }
        public List<string> WorkflowStates { get; init; }
        public bool PurchaseCompleted { get; set; }
    }
}