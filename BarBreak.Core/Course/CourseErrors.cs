namespace BarBreak.Core.Course;
using ErrorOr;

public static class CourseErrors
{
    public static ErrorOr<CourseDto> ValidationFailed =>
        Error.Validation("Course.ValidationFailed", "Course DTO validation failed");

    public static ErrorOr<CourseDto> Duplicate(string title) =>
        Error.Conflict("Course.Duplicate", $"Course with Title( {title} ) already exists");

    public static ErrorOr<CourseDto> NotFound(int id) =>
        Error.NotFound("Course.NotFound", $"Course with ID( {id} ) not found");
}