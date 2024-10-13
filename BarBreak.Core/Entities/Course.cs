using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BarBreak.Core.Entities
{
    public class Course
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}