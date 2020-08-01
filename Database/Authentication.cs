using System;
using System.Collections.Generic;

namespace Hostman.Database
{
    public partial class Authentication
    {
        public uint UserId { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }

        public virtual User User { get; set; }
    }
}
