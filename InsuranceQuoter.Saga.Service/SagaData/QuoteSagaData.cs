namespace InsuranceQuoter.Saga.Service.SagaData
{
    using NServiceBus;

    public class QuoteSagaData : ContainSagaData
    {
        public int RequestsSent { get; set; }
    }
}