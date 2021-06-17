namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using System.Collections.Generic;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Store.Payment;
    using Microsoft.AspNetCore.Components;

    public partial class PaymentProgress
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }

        [Inject]
        public IState<PaymentState> PaymentState { get; set; }

        public List<string> States => PaymentState.Value.WorkflowStates;
        public int StatesCompleted => PaymentState.Value.WorkflowStates.Count;
        public string StatesPercentage => PaymentState.Value.WorkflowStates.Count * 20 + "%";
        public bool PolicyBound => PaymentState.Value.PurchaseCompleted;
    }
}