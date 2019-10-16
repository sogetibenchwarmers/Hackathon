using System;
using System.Collections.Generic;
using System.Linq;

namespace HackathonApi.Models
{
    public class SubLocationList
    {
        public IEnumerable<SubLocation> Data { get; set; }

        public int Count => Data.Count();
    }
}
