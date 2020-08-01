using System.Threading.Tasks;
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
        public async Task<IActionResult> Put([FromBody] User user)
        {
            // Validating that the potentially modified nickname meets the
            // requirements

            if (user.Nickname.Length < Model.User.NICKNAME_MIN_LENGTH)
            {
                return this.BadRequest("Nickname too short!");
            }

            if (user.Nickname.Length > Model.User.NICKNAME_MAX_LENGTH)
            {
                return this.BadRequest("Nickname too long!");
            }

            // Saving the changes

            this.AuthenticatedUser.Nickname = user.Nickname;
            await this.DatabaseContext.SaveChangesAsync();

            return this.Ok(new User
            {
                Nickname = this.AuthenticatedUser.Nickname
            });
        }
    }
}
