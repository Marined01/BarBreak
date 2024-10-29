using BarBreak.Core.Course;


namespace BarBreak.Application.UseCases
{
    public class ViewCoursesUseCase
    {
        private readonly ICourseRepository _courseRepository;

        public ViewCoursesUseCase(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<CourseDto>> ExecuteAsync(int userId)
        {
            // Отримуємо курси з репозиторію
            var courses = await _courseRepository.GetCoursesForUserAsync(userId);

            // Мапимо `Course` на `CourseDto`
            var courseDtos = new List<CourseDto>();
            foreach (var course in courses)
            {
                courseDtos.Add(new CourseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description
                });
            }

            return courseDtos;
        }
    }
}