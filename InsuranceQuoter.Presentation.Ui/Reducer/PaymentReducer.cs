namespace InsuranceQuoter.Presentation.Ui.Reducer
{
    using System.Collections.Generic;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Models;
    using InsuranceQuoter.Presentation.Ui.Store.Payment;

    //TODO Refactor some duplication
    public class PaymentReducer
    {
        [ReducerMethod]
        public static PaymentState Handle(PaymentState _, InitializeStateAction __) =>
            new()
            {
                Model = new PaymentModel(),
                WorkflowStates = new List<string>(),
                PurchaseCompleted = false
            };

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, PaymentRequestedAction _) =>
            new()
            {
                Model = new PaymentModel()
                {
                    CardNumber = state.Model.CardNumber
                },
                PaymentProcessing = true,
                WorkflowStates = new List<string>()
            };

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, ReadyToContactPaymentProviderAction _) =>
            new()
            {
                Model = new PaymentModel
                {
                    CardNumber = state.Model.CardNumber
                },
                PaymentProcessing = true,
                WorkflowStates = new List<string>
                {
                    "1. Contacting payment provider to take payment..."
                }
            };

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, CardPaymentTakenAction _)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("2. Card has been authorised and payment has been successfully taken...");

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
        public static PaymentState Handle(PaymentState state, ReadyToBindPolicyWithInsurerAction _)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("3. Contacting selected Insurer to bind your policy...");

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
        public static PaymentState Handle(PaymentState state, PolicyBoundAction _)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("4. Policy has been successfully bound with the insurer...");

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
        public static PaymentState Handle(PaymentState state, ReadyToStorePolicyAction _)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("5. Preparing to store your policy details for future adjustments and renewal...");

            return new PaymentState
            {
                PolicyUid = state.PolicyUid,
                Model = new PaymentModel
                {
                    CardNumber = state.Model.CardNumber
                },
                WorkflowStates = workflowStates
            };
        }

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, PolicyPurchaseAndBindCompletedAction action)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("6. Policy stored for future adjustments and renewal");

            return new PaymentState()
            {
                PolicyUid = action.PolicyUid,
                InsurerName = action.InsurerName,
                Model = new PaymentModel
                {
                    CardNumber = state.Model.CardNumber
                },
                WorkflowStates = workflowStates,
                PurchaseCompleted = false
            };
        }

        [ReducerMethod]
        public static PaymentState Handle(PaymentState state, PurchaseOperationCompletedAction _)
        {
            var workflowStates = new List<string>();
            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            workflowStates.Add("7. Purchase and bind completed!");

            return new PaymentState() with
            {
                PolicyUid = state.PolicyUid,
                InsurerName = state.InsurerName,
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