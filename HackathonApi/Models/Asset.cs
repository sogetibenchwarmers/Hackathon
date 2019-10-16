using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Models
{
    public class Asset
    {
        public Guid Id { get; set; }

        public string AssetTag { get; set; }

        public string Name { get; set; }

        public User OwnedBy { get; set; }

        public Location Location { get; set; }

        public SubLocation SubLocation { get; set; }

        public string Status { get; set; }

        public SupportGroup SupportGroup { get; set; }

        public string AssignmentGroup { get; set; }
    }
}
