using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models
{
    [JsonObject]
    public class ServiceNowAssets
    {
        public IEnumerable<ServiceNowAsset> Result { get; set; }
    }
}
