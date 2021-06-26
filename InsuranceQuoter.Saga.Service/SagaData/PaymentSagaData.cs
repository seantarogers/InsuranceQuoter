namespace InsuranceQuoter.Saga.Service.SagaData
{
    using System;
    using NServiceBus;

    public class PaymentSagaData : ContainSagaData
    {
        public string SagaState { get; set; }
        public DateTime SagaStateDate { get; set; }
        public Guid QuoteUid { get; set; }
        public Guid PaymentUid { get; set; }
        public Guid PolicyUid { get; set; }
        public Guid CorrelationId { get; set; }
        public string UserName { get; set; }
        public string InsurerName { get; set; }
        public string Addons { get; set; }
        public decimal Amount { get; set; }
        public Guid RiskUid { get; set; }
    }
}