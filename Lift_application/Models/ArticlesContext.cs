using Microsoft.EntityFrameworkCore;

namespace Lift_application.Models
{
    public class ArticlesContext:DbContext
    {
        public DbSet<Articles> Articles { get; set; }
      
        public ArticlesContext(DbContextOptions<ArticlesContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
