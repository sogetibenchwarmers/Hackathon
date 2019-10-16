using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models
{
    public class ServiceNowResult<T>
    {
        public T Result { get; set; }
    }
}
