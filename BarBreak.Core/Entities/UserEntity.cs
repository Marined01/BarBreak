namespace BarBreak.Core.Entities;

public class UserEntity : BaseEntity<int>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public ICollection<CourseEntity> Courses { get; set; } = new List<CourseEntity>();

    public ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
}