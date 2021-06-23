namespace InsuranceQuoter.Saga.Service.SagaData
{
    using System;
    using NServiceBus;

    public class PaymentSagaData : ContainSagaData
    {
        public string SagaState { get; set; }
        public DateTime SagaStateDate { get; set; }
        public string QuoteReference { get; set; }
        public string PaymentReference { get; set; }
        public string PolicyReference { get; set; }
    }
}