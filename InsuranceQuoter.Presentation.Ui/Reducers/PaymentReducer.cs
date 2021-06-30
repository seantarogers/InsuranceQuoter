namespace InsuranceQuoter.Presentation.Ui.Reducers
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
        public static PaymentState Handle(PaymentState _, QuotesBackSelectedAction __) =>
            new()
            {
                Model = new PaymentModel(),
                WorkflowStates = new List<string>(),
                PurchaseCompleted = false
            };

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
                Model = new PaymentModel
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
            List<string> workflowStates = BuildExistingWorkflowStates(state);

            const string LatestState = "2. Card has been authorised and payment has been successfully taken...";
            AddLatestState(workflowStates, LatestState);

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
        public static PaymentState Handle(PaymentState state, ReadyToBindPolicyWithInsurerAction _)
        {
            List<string> workflowStates = BuildExistingWorkflowStates(state);

            const string LatestState = "3. Contacting selected Insurer to bind your policy...";
            AddLatestState(workflowStates, LatestState);

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
            List<string> workflowStates = BuildExistingWorkflowStates(state);

            const string LatestState = "4. Policy has been successfully bound with the insurer...";
            AddLatestState(workflowStates, LatestState);

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
            List<string> workflowStates = BuildExistingWorkflowStates(state);

            const string LatestState = "5. Preparing to store your policy details for future adjustments and renewal...";
            AddLatestState(workflowStates, LatestState);

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
            List<string> workflowStates = BuildExistingWorkflowStates(state);

            const string LatestState = "6. Policy stored for future adjustments and renewal";
            AddLatestState(workflowStates, LatestState);

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
            List<string> workflowStates = BuildExistingWorkflowStates(state);

            const string LatestState = "7. Purchase and bind completed!";
            AddLatestState(workflowStates, LatestState);

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

        private static void AddLatestState(ICollection<string> workflowStates, string latestState)
        {
            if (!workflowStates.Contains(latestState))
            {
                workflowStates.Add(latestState);
            }
        }

        private static List<string> BuildExistingWorkflowStates(PaymentState state)
        {
            var workflowStates = new List<string>();

            foreach (string workflowState in state.WorkflowStates)
            {
                workflowStates.Add(workflowState);
            }

            return workflowStates;
        }
    }
}