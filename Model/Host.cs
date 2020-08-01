
using System;
using System.Net;

namespace Hostman.Model
{
    public class Host
    {
        public static Host From(Database.Host dbHost) => new Host
        {
            Id = dbHost.Id,
            Name = dbHost.Name,
            IPMode = Enum.Parse<IPMode>(dbHost.Ipmode),
            AssignedIP = (dbHost.AssignedIp.HasValue)
                ? new IPAddress(IPAddress.NetworkToHostOrder(Convert.ToInt32(
                    dbHost.AssignedIp.Value))).ToString()
                : null
        };

        public uint? Id { get; set; }

        public string Name { get; set; }

        public IPMode IPMode { get; set; }

        public string AssignedIP { get; set; }
    }
}
