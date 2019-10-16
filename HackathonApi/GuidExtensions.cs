using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi
{
    public static class GuidExtensions
    {
        public static string ToUuid(this Guid value)
        {
            return value.ToString().Replace("-", string.Empty);
        }
    }
}
