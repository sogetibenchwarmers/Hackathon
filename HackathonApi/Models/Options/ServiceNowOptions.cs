using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models.Options
{
    public class ServiceNowOptions
    {
        public string ServiceNowHost { get; set; }

        public string ServiceNowUser { get; set; }

        public string ServiceNowSecret { get; set; }
    }
}
