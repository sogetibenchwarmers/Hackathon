using Newtonsoft.Json;

namespace HackathonApi.Models
{
    public class ServiceNowAssetPatchRequest
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("support_group")]
        public string SupportGroup { get; set; }

        [JsonProperty("owned_by")]
        public string OwnedBy { get; set; }
    }
}
