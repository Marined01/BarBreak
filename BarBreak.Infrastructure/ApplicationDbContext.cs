using Microsoft.EntityFrameworkCore;
using BarBreak.Core; 

public class ApplicationDbContext : DbContext
{
    // Набори даних для сутностей
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Role> Roles { get; set; }

    // Конфігурація з'єднання
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("DB_CONNECTION_STRING environment variable is not set.");
        }
        optionsBuilder.UseNpgsql(connectionString);
    }

    // Конфігурація моделі
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Визначення зв'язків між сутностями
        modelBuilder.Entity<User>()
            .HasMany(u => u.Courses)
            .WithMany(c => c.Users)
            .UsingEntity(j => j.ToTable("UserCourses")); // Проміжна таблиця для зв'язку User-Course

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles")); // Проміжна таблиця для зв'язку User-Role
    }
}
