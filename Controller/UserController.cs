using System.Threading.Tasks;
using Hostman.Database;
using Hostman.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hostman.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(new User
            {
                Nickname = this.AuthenticatedUser.Nickname
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Model.User user)
        {
            this.AuthenticatedUser.Nickname = user.Nickname;
            await this.DatabaseContext.SaveChangesAsync();

            return this.Ok(new User
            {
                Nickname = this.AuthenticatedUser.Nickname
            });
        }
    }
}
