using System;

namespace HackathonApi.Models
{
    public class AssetPutRequest
    {
        public string AssetTag { get; set; }

        public string Name { get; set; }

        public string OwnedBy { get; set; }

        public Guid LocationId { get; set; }

        public Guid SubLocationId { get; set; }

        public string Status { get; set; }

        public Guid SupportGroupId { get; set; }

        public string AssignmentGroup { get; set; }
    }
}