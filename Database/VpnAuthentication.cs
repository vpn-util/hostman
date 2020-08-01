using System;
using System.Collections.Generic;

namespace Hostman.Database
{
    public partial class VpnAuthentication
    {
        public uint HostId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Expiration { get; set; }

        public virtual Host Host { get; set; }
    }
}
