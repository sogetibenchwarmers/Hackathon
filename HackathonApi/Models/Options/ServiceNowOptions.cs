using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models.Options
{
    public class ServiceNowOptions
    {
        public virtual string ServiceNowHost { get; set; }

        public virtual string ServiceNowUser { get; set; }

        public virtual string ServiceNowSecret { get; set; }
    }
}
