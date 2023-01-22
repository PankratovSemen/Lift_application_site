using Microsoft.EntityFrameworkCore;

namespace Lift_application.Models
{
    public class EmailForSendContext:DbContext
    {
        public DbSet<EmailForSend> EmailForSend { get; set; }
        public EmailForSendContext(DbContextOptions<EmailForSendContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        
    }
}
