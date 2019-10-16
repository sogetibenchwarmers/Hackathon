using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models
{
    public class ServiceNowListResult<T>
    {
        public IEnumerable<T> Result { get; set; }
    }
}
