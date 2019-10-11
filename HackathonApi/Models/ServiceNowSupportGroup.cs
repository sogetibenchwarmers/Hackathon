using Newtonsoft.Json;
using System;

namespace HackathonApi.Models
{
    public class ServiceNowSupportGroup
    {
        [JsonProperty("sys_id")]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
