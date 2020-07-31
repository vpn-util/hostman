using Microsoft.AspNetCore.Mvc;

namespace Hostman.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HostsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok("TODO: Returns hosts");
        }
    }
}
