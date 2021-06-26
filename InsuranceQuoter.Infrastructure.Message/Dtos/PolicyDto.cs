namespace InsuranceQuoter.Infrastructure.Message.Dtos
{
    using System;
    using Newtonsoft.Json;

    public class PolicyDto : Dto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("addressUid")]
        public Guid AddressUid { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("registration")]
        public string Registration { get; set; }

        [JsonProperty("coverType")]
        public string CoverType { get; set; }

        [JsonProperty("expiresOn")]
        public DateTime ExpiresOn { get; set; }

        [JsonProperty("insurerName")]
        public string InsurerName { get; set; }

        [JsonProperty("addons")]
        public string AddOns { get; set; }
    }
}