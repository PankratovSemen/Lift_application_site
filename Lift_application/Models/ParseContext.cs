using Microsoft.EntityFrameworkCore;
namespace Lift_application.Models
{
    public class ParseContext:DbContext
    {
        public DbSet<ParseModel> parses { get; set; }
        public ParseContext(DbContextOptions<ParseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
