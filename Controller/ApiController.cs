using System;
using Hostman.Database;
using Hostman.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace Hostman.Controller
{
    /// <summary>
    /// An abstract controller that provides additional utilities for its child
    /// classes.
    /// </summary>
    public abstract class ApiController : ControllerBase
    {
        /// <summary>
        /// The database identity of the user that performs the current
        /// request.
        /// </summary>
        protected Database.Model.User AuthenticatedUser
        {
            get
            {
                var dbUser = this.HttpContext.Items[UserMiddleware.USER_MODEL]
                    as Database.Model.User;

                if (dbUser == null)
                {
                    throw new InvalidOperationException(
                        "No authenticated user!");
                }

                return dbUser;
            }
        }

        /// <summary>
        /// The current database context.
        /// </summary>
        protected Context DatabaseContext
        {
            get => (Context) this.HttpContext.RequestServices.GetService(
                typeof(Context));
        }
    }
}
