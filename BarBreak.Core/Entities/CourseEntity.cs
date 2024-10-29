namespace BarBreak.Core.Entities;

public class CourseEntity : BaseEntity<int>
{
    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}