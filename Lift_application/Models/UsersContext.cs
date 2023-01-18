using Microsoft.EntityFrameworkCore;

namespace Lift_application.Models
{
    public class UsersContext:DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
