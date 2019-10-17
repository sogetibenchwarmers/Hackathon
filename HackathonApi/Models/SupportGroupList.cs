using System.Collections.Generic;
using System.Linq;

namespace HackathonApi.Models
{
    public class SupportGroupList
    {
        public IEnumerable<SupportGroup> Data { get; set; }

        public int Count => Data.Count();
    }
}
