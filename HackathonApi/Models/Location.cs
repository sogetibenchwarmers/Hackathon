
using System;

namespace HackathonApi.Models
{
    public class Location
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        internal int Left { get; set; }

        internal int Right { get; set; }
    }
}
