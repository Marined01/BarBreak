using BarBreak.Core.DTOs;
using BarBreak.Core.Repositories;

namespace BarBreak.Application.UseCases
{
    public class ViewCourseUseCase
    {
        private readonly ICourseRepository _courseRepository;

        public ViewCourseUseCase(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public ResultDto<CourseDto> Execute(int courseId)
        {
            var course = _courseRepository.GetCourseById(courseId);

            if (course == null)
            {
                return new ResultDto<CourseDto>
                {
                    Success = false,
                    Errors = new List<string> { "Course not found." }
                };
            }

            var courseDto = new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description
            };

            return new ResultDto<CourseDto>
            {
                Success = true,
                Data = courseDto
            };
        }
    }
}