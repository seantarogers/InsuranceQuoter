namespace InsuranceQuoter.Infrastructure.Message.Dtos
{
    using Newtonsoft.Json;

    public abstract class Dto
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}