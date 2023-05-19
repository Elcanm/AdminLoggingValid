using Microsoft.EntityFrameworkCore;

namespace AdminLoggingValid.Model
{
    public class AdminDbContext:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            : base(options)
        {

        }
    }
}
