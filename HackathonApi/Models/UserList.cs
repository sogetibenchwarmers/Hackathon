using System.Collections.Generic;
using System.Linq;

namespace HackathonApi.Models
{
    public class UserList
    {
        public IEnumerable<User> Data { get; set; }

        public int Count => Data.Count();
    }
}
