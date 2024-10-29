using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BarBreak.Core.Repositories;
using BarBreak.Core.DTOs;
using BarBreak.Application.Validators;
using BarBreak.Core.Entities;

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
