using Newtonsoft.Json;

namespace HackathonApi.Models
{
    public class ServiceNowAssetPatchRequest
    {
        [JsonProperty("asset_tag")]
        public string AssetTag { get; set; }

        public string Location { get; set; }

        [JsonProperty("support_group")]
        public string SupportGroup { get; set; }
    }
}
