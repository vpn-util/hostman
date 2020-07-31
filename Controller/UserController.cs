using Microsoft.AspNetCore.Mvc;

namespace Hostman.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok("TODO: Return user data");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return this.Ok("TODO: Update user data");
        }
    }
}
