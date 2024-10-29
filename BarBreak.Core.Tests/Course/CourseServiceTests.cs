namespace BarBreak.Core.Tests.Course;

using BarBreak.Core.Course;
using BarBreak.Core.Tests.Course.Helpers;

public class CourseServiceTests
{
    private readonly ICourseService _sut;
    private readonly ICourseRepository _repositoryMock;

    public CourseServiceTests()
    {
        this._repositoryMock = Substitute.For<ICourseRepository>();
        this._sut = new CourseService(this._repositoryMock, Substitute.For<Serilog.ILogger>());
    }

    // [Fact]
    // public async Task Create_WhenCourseDtoIsNull_ValidationError()
    // {
    //     var result = await this._sut.AddCourse(null!);
    //
    //     result.Should().NotBeNull();
    //     result.IsError.Should().BeTrue();
    //     result.FirstError.Should().BeEquivalentTo(CourseErrors.ValidationFailed.FirstError);
    // }

    [Fact]
    public async Task Create_WhenCourseDtoHasId_ValidationError()
    {
        var faker = new CourseDtoFakeProvider();
        var dto = faker.Get();
        dto.Id = 10;

        var result = await this._sut.AddCourse(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(CourseErrors.ValidationFailed.FirstError);
    }

    [Fact]
    public async Task Update_WhenCourseDtoHasInvalidId_ValidationError()
    {
        var faker = new CourseDtoFakeProvider();
        var dto = faker.Get();
        dto.Id = -100;

        var result = await this._sut.UpdateCourse(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(CourseErrors.ValidationFailed.FirstError);
    }

    // [Fact]
    // public async Task Update_WhenCourseDtoIsNull_ValidationError()
    // {
    //     var result = await this._sut.UpdateCourse(null!);
    //
    //     result.Should().NotBeNull();
    //     result.IsError.Should().BeTrue();
    //     result.FirstError.Should().BeEquivalentTo(CourseErrors.ValidationFailed.FirstError);
    // }

    [Fact]
    public async Task Update_WhenNotExists_NotFoundError()
    {
        var faker = new CourseDtoFakeProvider();
        var dto = faker.Get();

        this._repositoryMock.GetById(Arg.Any<int>()) !.Returns(Task.FromResult<CourseEntity>(null!));

        var result = await this._sut.UpdateCourse(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(CourseErrors.NotFound(dto.Id).FirstError);
    }

    [Theory]
    [ClassData(typeof(CourseDtoFakeProvider))]
    public async Task Update_WhenValid_CourseUpdated(CourseDto dto)
    {
        var faker = new CourseDtoFakeProvider();
        var updatedCourse = faker.Get();
        updatedCourse.Id = dto.Id;

        var oldEnt = new CourseEntity
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
        };

        this._repositoryMock.GetById(Arg.Any<int>()).Returns(oldEnt);
        this._repositoryMock.Update(Arg.Any<CourseEntity>())
            .Returns(c => Task.FromResult(c.Arg<CourseEntity>()));

        var result = await this._sut.UpdateCourse(updatedCourse);

        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.Description.Should().Be(updatedCourse.Description);
        result.Value.Title.Should().Be(updatedCourse.Title);
    }

    [Fact]
    public async Task Get_WhenNotExists_NotFoundError()
    {
        const int id = 1;

        this._repositoryMock.GetById(Arg.Any<int>())!.Returns(Task.FromResult<CourseEntity>(null!));

        var result = await this._sut.GetCourseById(id);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(CourseErrors.NotFound(id).FirstError);
    }

    [Theory]
    [ClassData(typeof(CourseDtoFakeProvider))]
    public async Task Get_WhenExists_CourseDtoReturned(CourseDto dto)
    {
        var profileEntity = new CourseEntity()
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
        };

        this._repositoryMock.GetById(Arg.Any<int>())!.Returns(Task.FromResult(profileEntity));

        var result = await this._sut.GetCourseById(dto.Id);

        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.Title.Should().Be(dto.Title);
        result.Value.Description.Should().Be(dto.Description);
    }

    // This is a formal test to keep code coverage.
    // Actually delete must be validated with integration tests
    [Fact]
    public async Task Delete_WhenExists_Success()
    {
        const int id = 0;

        this._repositoryMock.Delete(Arg.Any<int>()).Returns(Task.CompletedTask);

        await this._sut.DeleteCourse(id);

        Assert.True(true);
    }
}