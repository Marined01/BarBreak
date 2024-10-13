using System.Collections.Generic;

namespace BarBreak.Core.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
