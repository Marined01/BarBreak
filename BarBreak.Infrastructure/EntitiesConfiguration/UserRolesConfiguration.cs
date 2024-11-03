namespace BarBreak.Infrastructure.EntitiesConfiguration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

// it's better to move the extra logic outside your adapted DbContext
// for more information about EF Core - Ef core in Action
public class UserRolesConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        // Definition of relationships between entities
        builder
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));
    }
}