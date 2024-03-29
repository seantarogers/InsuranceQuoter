﻿namespace InsuranceQuoter.Infrastructure.Message.Dtos
{
    using System;
    using Newtonsoft.Json;

    public class AddressDto : Dto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }
    }
}