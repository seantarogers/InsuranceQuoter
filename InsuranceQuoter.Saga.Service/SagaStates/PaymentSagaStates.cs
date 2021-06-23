namespace InsuranceQuoter.Saga.Service.SagaStates
{
    public static class PaymentSagaStates
    {
        public const string SagaStarted = "Payment Saga started";
        public const string AuthorisingPayment = "Authorising Payment";
        public const string PaymentAuthorisedAndTaken = "Payment Taken";
        public const string PolicyBinding = "Submitting policy to insurer for binding";
        public const string PolicyBound = "Policy bound with insurer";
        public const string AddingPolicy = "Adding policy to Insurance Quoter";
        public const string PolicyAdded = "Policy added to Insurance Quoter";
    }
}