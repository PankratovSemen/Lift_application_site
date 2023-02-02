using Microsoft.EntityFrameworkCore;

namespace Lift_application.Models
{
    public class ParseArticlesContext:DbContext
    {
        public DbSet<ParseArticlesModel> ParseArticles { get; set; }
        public ParseArticlesContext(DbContextOptions<ParseArticlesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
