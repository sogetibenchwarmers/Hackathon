using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models
{
    [JsonObject]
    public class ServiceNowAsset
    {
        [JsonProperty("sys_id")]
        public Guid Id { get; set; }
        
        [JsonProperty("asset_tag")]
        public string AssetTag { get; set; }

        [JsonProperty("display_name")]
        public string Name { get; set; }

        [JsonProperty("owned_by")]
        public RelatedContent OwnedBy { get; set; }

        public RelatedContent Location { get; set; }

        public RelatedContent Status { get; set; }

        [JsonProperty("support_group")]
        public RelatedContent SupportGroup { get; set; }

        public RelatedContent AssignmentGroup { get; set; }
    }
}
