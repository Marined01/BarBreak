using Microsoft.EntityFrameworkCore;
using BarBreak.Core.Entities;

public class ApplicationDbContext : DbContext
{
    // Entity datasets
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Role> Roles { get; set; }

    // Connection configuration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("DB_CONNECTION_STRING environment variable is not set.");
        }
        optionsBuilder.UseNpgsql(connectionString);
    }

    // Model configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Визначення зв'язків між сутностями
        modelBuilder.Entity<User>()
            .HasMany(u => u.Courses)
            .WithMany(c => c.Users)
            .UsingEntity(j => j.ToTable("UserCourses"));

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));
    }
}
