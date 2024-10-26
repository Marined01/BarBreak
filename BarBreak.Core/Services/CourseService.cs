using BarBreak.Core.DTOs;
using BarBreak.Core.Entities;
using BarBreak.Core.Repositories;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace BarBreak.Core.Services
{
    public interface ICourseService
    {
        CourseDto GetCourseById(int id);
        IEnumerable<CourseDto> GetAllCourses();
        void AddCourse(CourseDto course);
        void UpdateCourse(CourseDto course);
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

        public CourseDto GetCourseById(int id)
        {
            _logger.Information("Fetching course with ID: {CourseId}", id);
            var course = _courseRepository.GetCourseById(id);

            return new CourseDto
            {
                Id = course.ID,
                Title = course.Title,
                Description = course.Description
            };
        }

        public IEnumerable<CourseDto> GetAllCourses()
        {
            _logger.Information("Fetching all courses");
            var courses = _courseRepository.GetAllCourses();

            return courses.Select(course => new CourseDto
            {
                Id = course.ID,
                Title = course.Title,
                Description = course.Description
            }).ToList();
        }

        public void AddCourse(CourseDto courseDto)
        {
            _logger.Information("Adding new course with title: {CourseTitle}", courseDto.Title);

            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description
            };

            _courseRepository.AddCourse(course);
        }

        public void UpdateCourse(CourseDto courseDto)
        {
            _logger.Information("Updating course with ID: {CourseId}", courseDto.Id);

            var course = _courseRepository.GetCourseById(courseDto.Id);

            course.Title = courseDto.Title;
            course.Description = courseDto.Description;

            _courseRepository.UpdateCourse(course);
        }

        public void DeleteCourse(int id)
        {
            _logger.Information("Deleting course with ID: {CourseId}", id);
            _courseRepository.DeleteCourse(id);
        }
    }
}
