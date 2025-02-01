using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kuba_web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<TodoTask> Tasks { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TodoTask>().HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId);

    }
}
