using System.Linq;
using System.Threading.Tasks;
using Hostman.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hostman.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HostController : ApiController
    {
        [HttpPost]
        public IActionResult Create()
        {
            return this.Ok("TODO: Create host");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var host = await this.DatabaseContext.Entry(this.AuthenticatedUser)
                .Collection(u => u.Host)
                .Query()
                .Where(h => h.Id == id)
                .Select(h => Host.From(h))
                .SingleOrDefaultAsync();

            if (host == null)
                return this.NotFound();

            return this.Ok(host);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id)
        {
            return this.Ok("TODO: Update host");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return this.Ok("TODO: Delete host");
        }

        [HttpPost("{id:int}/prepare")]
        public IActionResult Prepare(int id)
        {
            return this.Ok("TODO: Prepare host");
        }
    }
}
