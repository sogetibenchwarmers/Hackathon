using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models
{
    public class LocationList
    {
        public IEnumerable<Location> Data { get; set; }

        public int Count => Data.Count();
    }
}
