namespace InsuranceQuoter.Presentation.Ui.Reducer
{
    using System.Collections.Generic;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Payment;

    public class PaymentReducer
    {
        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, InitializeStateAction action) =>
            new PaymentState()
            {
                Model = new PaymentModel(),
                WorkflowStates = new List<string>(),
                PurchaseCompleted = false
            };

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, PaymentRequestedAction action) =>
            new PaymentState()
            {
                Model = new PaymentModel()
                {
                    CardNumber = state.Model.CardNumber
                },
                PaymentProcessing = true,
                WorkflowStates = new List<string>()
            };

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, PaymentProviderContactedAction action) =>
            new PaymentState()
            {
                Model = new PaymentModel()
                {
                    CardNumber = state.Model.CardNumber
                },
                PaymentProcessing = true,
                WorkflowStates = new List<string>
                {
                    "Payment provider contacted"
                }
            };

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, CardAuthorisedAction action)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("Card has been authorised");

            return new PaymentState()
            {
                Model = new PaymentModel()
                {
                    CardNumber = state.Model.CardNumber
                },
                WorkflowStates = workflowStates
            };
        }

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, PaymentTakenAction action)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("Payment has been taken");

            return new PaymentState()
            {
                Model = new PaymentModel
                {
                    CardNumber = state.Model.CardNumber
                },
                WorkflowStates = workflowStates
            };
        }

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, InsurerContactedAction action)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("Contacting Insurer to bind policy");

            return new PaymentState
            {
                Model = new PaymentModel
                {
                    CardNumber = state.Model.CardNumber
                },
                WorkflowStates = workflowStates
            };
        }

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, PolicyBoundAction action)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("Policy successfully bound");

            return new PaymentState()
            {
                PolicyReference = action.ReferenceNumber,
                Model = new PaymentModel
                {
                    CardNumber = state.Model.CardNumber
                },
                WorkflowStates = workflowStates,
                PurchaseCompleted = false
            };
        }

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, PurchaseCompletedAction action)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("Purchase completed");

            return new PaymentState()
            {
                PolicyReference = state.PolicyReference,
                Model = new PaymentModel
                {
                    CardNumber = state.Model.CardNumber
                },
                WorkflowStates = workflowStates,
                PurchaseCompleted = true
            };
        }
    }
}