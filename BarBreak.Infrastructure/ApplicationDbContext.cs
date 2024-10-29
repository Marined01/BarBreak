namespace BarBreak.Infrastructure;

public class ApplicationDbContext : DbContext
{
    // Entity datasets
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<CourseEntity> Courses { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }

    /// Connection configuration
    /// <inheritdoc/>
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}