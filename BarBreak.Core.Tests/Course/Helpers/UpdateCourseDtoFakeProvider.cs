namespace BarBreak.Core.Tests.Course.Helpers;

using BarBreak.Core.Course;
using BarBreak.Core.Tests.Helpers;

public class UpdateCourseDtoFakeProvider() : ObjectFakeProviderBase<CourseDto>(5)
{
    protected override Func<Faker<CourseDto>> DefaultFactory =>
        () => new Faker<CourseDto>()
            .RuleFor(x => x.Id, f => f.IndexFaker)
            .RuleFor(x => x.Title, f => f.Name.JobTitle())
            .RuleFor(x => x.Description, f => f.Name.JobDescriptor());
}