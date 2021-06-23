namespace InsuranceQuoter.Infrastructure.Message.Events
{
    public class PolicyBoundEvent : Message
    {
        public string PolicyReference { get; set; }
    }
}