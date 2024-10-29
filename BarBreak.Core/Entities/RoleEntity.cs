namespace BarBreak.Core.Entities;

public class RoleEntity : BaseEntity<int>
{
    public string RoleName { get; set; }

    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}