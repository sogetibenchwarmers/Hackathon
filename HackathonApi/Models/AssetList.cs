using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models
{
    public class AssetList
    {
        public IEnumerable<Asset> Data { get; set; }

        public int Count => Data.Count();
    }
}
