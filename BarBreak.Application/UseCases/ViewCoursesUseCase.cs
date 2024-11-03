using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarBreak.Core.Repositories; // для ICourseRepository
using BarBreak.Core.DTOs; // для CourseDto
using BarBreak.Application.Validators;
using BarBreak.Core.Entities;
using BarBreak.Core.Course;

namespace BarBreak.Application.UseCases
{
    public class ViewCoursesUseCase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ViewCoursesValidator _validator;

        public ViewCoursesUseCase(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _validator = new ViewCoursesValidator();
        }

        public async Task<List<CourseDto>> ExecuteAsync(int userId)
        {
            // Виконуємо валідацію userId
            var validationResult = _validator.Validate(userId);
            if (!validationResult.IsValid)
            {
                // Повертаємо порожній список, якщо валідація не пройшла
                return new List<CourseDto>();
            }

            // Отримуємо курси з репозиторію
            var courses = await _courseRepository.GetCoursesForUserAsync(userId);

            // Мапимо `Course` на `CourseDto`
            var courseDtos = courses.Select(course => new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description
            }).ToList();

            return courseDtos;
        }
    }
}
