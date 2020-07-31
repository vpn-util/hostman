using Microsoft.AspNetCore.Mvc;

namespace Hostman.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HostController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create()
        {
            return this.Ok("TODO: Create host");
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return this.Ok("TODO: Return host");
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
