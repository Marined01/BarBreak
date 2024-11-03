namespace BarBreak.Core.Course;

using BarBreak.Core.Entities;
using Serilog;
using ErrorOr;

public interface ICourseService
{
    Task<ErrorOr<CourseDto>> GetCourseById(int id);

    Task<IEnumerable<CourseDto>> GetAllCourses();

    Task<ErrorOr<CourseDto>> AddCourse(CourseDto course);

    Task<ErrorOr<CourseDto>> UpdateCourse(CourseDto course);

    Task DeleteCourse(int id);
}

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger _logger;

    public CourseService(ICourseRepository courseRepository, ILogger logger)
    {
        this._courseRepository = courseRepository;
        this._logger = logger;
    }

    // User
    public async Task<ErrorOr<CourseDto>> GetCourseById(int id)
    {
        this._logger.Information("Fetching course with ID: {CourseId}", id);
        var course = await this._courseRepository.GetById(id);

        if (course is null)
        {
            return CourseErrors.NotFound(id);
        }

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
        };
    }

    // User
    public async Task<IEnumerable<CourseDto>> GetAllCourses()
    {
        this._logger.Information("Fetching all courses");
        var courses = await this._courseRepository.GetAll();

        return courses.Select(course => new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
        });
    }

    // Admin
    public async Task<ErrorOr<CourseDto>> AddCourse(CourseDto courseDto)
    {
        if (courseDto.Id is not 0)
        {
            return CourseErrors.ValidationFailed;
        }

        this._logger.Information("Adding new course with title: {CourseTitle}", courseDto.Title);

        var course = new CourseEntity
        {
            Title = courseDto.Title,
            Description = courseDto.Description,
        };

        var entity = await this._courseRepository.Create(course);

        return new CourseDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }

    // Admin
    public async Task<ErrorOr<CourseDto>> UpdateCourse(CourseDto courseDto)
    {
        if (courseDto.Id <= 0)
        {
            return CourseErrors.ValidationFailed;
        }

        this._logger.Information("Updating course with ID: {CourseId}", courseDto.Id);

        var course = await this._courseRepository.GetById(courseDto.Id);

        if (course is null)
        {
            return CourseErrors.NotFound(courseDto.Id);
        }

        course.Title = courseDto.Title;
        course.Description = courseDto.Description;

        await this._courseRepository.Update(course);

        return courseDto;
    }

    // Admin
    public async Task DeleteCourse(int id)
    {
        this._logger.Information("Deleting course with ID: {CourseId}", id);
        await this._courseRepository.Delete(id);
    }
}