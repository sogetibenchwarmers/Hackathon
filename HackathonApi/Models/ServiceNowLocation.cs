using Newtonsoft.Json;
using System;

namespace HackathonApi.Models
{
    [JsonObject]
    public class ServiceNowLocation
    {
        [JsonProperty("sys_id")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public RelatedContent Parent { get; set; }

        internal int Left { get; set; }

        internal int Right { get; set; }

        internal bool isLeaf => Left + 1 == Right;
    }
}
