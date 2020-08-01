using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hostman.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hostman.Middleware
{
    public class UserMiddleware
    {
        public static readonly string USER_MODEL = typeof(User).FullName;

        private readonly RequestDelegate _NextHandler;

        public UserMiddleware(RequestDelegate nextHandler)
        {
            this._NextHandler = nextHandler;
        }

        public async Task InvokeAsync(HttpContext ctx, Context dbContext)
        {
            // Users should be always authenticated at this point

            if (!ctx.User.Identity.IsAuthenticated)
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            // Extracting the unique identifier and its issuer from the user

            var subClaim = ctx.User.FindAll(ClaimTypes.NameIdentifier)
                .Single();

            var subject = subClaim.Value;
            var issuer = subClaim.Issuer;

            // Resolving the user object by its authentication information from
            // the database

            var authentication = await dbContext.Authentication.Include(
                    a => a.User)
                .Where(a => a.Issuer == issuer)
                .Where(a => a.Subject == subject)
                .SingleOrDefaultAsync();

            if (authentication == null)
            {
                // NOTE: At this point, the user has been authenticated by the
                //       Open ID Connect provider, but does not exist in the
                //       database.
                //       At the moment, every user needs to be registered
                //       manually.
                //
                // TODO: Develop a concept of a secure user registration.

                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            // Storing the user instance, that is associated with the
            // authentication, inside the current HttpContext.

            ctx.Items[UserMiddleware.USER_MODEL] = authentication.User;

            // Handing over to the next middleware

            await this._NextHandler(ctx);
        }
    }
}
