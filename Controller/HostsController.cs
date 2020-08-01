using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hostman.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hostman.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HostsController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hosts = await this.DatabaseContext.Entry(
                    this.AuthenticatedUser)
                .Collection(u => u.Host)
                .Query()
                .Select(h => Host.From(h))
                .ToListAsync();

            return this.Ok(hosts);
        }
    }
}
