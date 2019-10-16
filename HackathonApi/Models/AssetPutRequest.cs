using System;

namespace HackathonApi.Models
{
    public class AssetPutRequest
    {
        public Guid Id { get; set; }

        public Guid LocationId { get; set; }

        public Guid SubLocationId { get; set; }

        public Guid SupportGroupId { get; set; }

        public Guid OwnedById { get; set; }

        public string AssignmentGroup { get; set; }
    }
}