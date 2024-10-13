using BarBreak.Core.Entities;
using BarBreak.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace BarBreak.Core.Services
{
    public interface ICourseService
    {
        Course GetCourseById(int id);
        IEnumerable<Course> GetAllCourses();
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
    }

    internal class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger _logger;

        public CourseService(ICourseRepository courseRepository, ILogger logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public Course GetCourseById(int id)
        {
            try
            {
                _logger.Information("Fetching course with ID: {CourseId}", id);
                var course = _courseRepository.GetCourseById(id);
                return course;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching course with ID: {CourseId}", id);
                throw;
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            try
            {
                _logger.Information("Fetching all courses");
                var courses = _courseRepository.GetAllCourses();
                return courses;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching all courses");
                throw;
            }
        }

        public void AddCourse(Course course)
        {
            try
            {
                _logger.Information("Adding new course with title: {CourseTitle}", course.Title);
                _courseRepository.AddCourse(course);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding course with title: {CourseTitle}", course.Title);
                throw;
            }
        }

        public void UpdateCourse(Course course)
        {
            try
            {
                _logger.Information("Updating course with ID: {CourseId}", course.ID);
                _courseRepository.UpdateCourse(course);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating course with ID: {CourseId}", course.ID);
                throw;
            }
        }

        public void DeleteCourse(int id)
        {
            try
            {
                _logger.Information("Deleting course with ID: {CourseId}", id);
                _courseRepository.DeleteCourse(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting course with ID: {CourseId}", id);
                throw;
            }
        }
    }
}
