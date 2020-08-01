using System;
using System.Collections.Generic;

namespace Hostman.Database
{
    public partial class User
    {
        public User()
        {
            Host = new HashSet<Host>();
        }

        public uint Id { get; set; }
        public string Nickname { get; set; }

        public virtual ICollection<Host> Host { get; set; }
    }
}
