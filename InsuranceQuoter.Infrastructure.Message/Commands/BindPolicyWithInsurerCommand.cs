namespace InsuranceQuoter.Infrastructure.Message.Commands
{
    public class BindPolicyWithInsurerCommand : Message
    {
        public string QuoteReference { get; set; }
    }
}