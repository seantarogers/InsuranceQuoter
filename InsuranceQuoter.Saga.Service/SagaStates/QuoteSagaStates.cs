namespace InsuranceQuoter.Saga.Service.SagaStates
{
    public static class QuoteSagaStates
    {
        public const string SagaStarted = "Quote Saga started";
        public const string QuoteReceived = "Quote {0} of {1} received";
        public const string RiskAdded = "Risk has been added to the database";
    }
}