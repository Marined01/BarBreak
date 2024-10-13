using BarBreak.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBreak.Core.Repositories
{
    public interface ICourseRepository
    {
        Course GetCourseById(int id);
        IEnumerable<Course> GetAllCourses();
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
    }
}
