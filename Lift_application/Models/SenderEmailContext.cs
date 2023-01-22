using Microsoft.EntityFrameworkCore;
namespace Lift_application.Models
{
    public class SenderEmailContext : DbContext
    {
        public DbSet<SenderEmailModel> ArticlesSender { get; set; }
        public SenderEmailContext(DbContextOptions<SenderEmailContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}