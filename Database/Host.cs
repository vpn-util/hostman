using System;
using System.Collections.Generic;

namespace Hostman.Database
{
    public partial class Host
    {
        public uint Id { get; set; }
        public uint Owner { get; set; }
        public string Name { get; set; }
        public string Ipmode { get; set; }
        public uint? AssignedIp { get; set; }

        public virtual User OwnerNavigation { get; set; }
    }
}
