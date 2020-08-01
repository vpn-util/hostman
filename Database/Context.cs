using Hostman.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Hostman.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Authentication> Authentications { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
    }
}
