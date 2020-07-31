
using System.Collections.Generic;

namespace Hostman.Configuration
{
    public class OpenIDConnectOptions
    {
        /// <summary>
        /// The URL to the OpenID Connect discovery file.
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// The unique identifiers of all valid OpenID Connect audiences.
        /// </summary>
        public ICollection<string> ValidAudiences { get; set; }

        /// <summary>
        /// Overrides the valid issuers that may have been specified inside the
        /// OpenID Connect discovery file.
        /// </summary>
        public ICollection<string> ValidIssuers { get; set; }

        /// <summary>
        /// Overrides the valid signing keys that may been specified inside the
        /// OpenID Connect discovery file.
        /// </summary>
        public ICollection<string> IssuerSigningKeys { get; set; }
    }
}
