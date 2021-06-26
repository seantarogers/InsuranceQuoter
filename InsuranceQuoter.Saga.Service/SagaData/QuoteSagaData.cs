namespace InsuranceQuoter.Saga.Service.SagaData
{
    using System;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using NServiceBus;

    public class QuoteSagaData : ContainSagaData
    {
        public string SagaState { get; set; }
        public DateTime SagaStateDate { get; set; }
        public Guid RiskUid { get; set; }
        public int NumberOfInsurerRequestsSent { get; set; }
        public int NumberOfInsurerRequestsReceived { get; set; }
        public QuoteRequest QuoteRequest { get; set; }
        public Guid CorrelationId { get; set; }
        public bool RiskAdded { get; set; }
    }
}