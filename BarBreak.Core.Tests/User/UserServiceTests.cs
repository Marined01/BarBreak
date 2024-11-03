namespace BarBreak.Core.Tests.User;

using BarBreak.Core.User;

public class UserServiceTests
{
    private readonly IUserService _sut;
    private readonly IUserRepository _repositoryMock;

    public UserServiceTests()
    {
        this._repositoryMock = Substitute.For<IUserRepository>();
        this._sut = new UserService(this._repositoryMock, Substitute.For<Serilog.ILogger>());
    }

    [Fact]
    public async Task Create_WhenUserDtoHasId_ValidationError()
    {
        var faker = new UserDtoFakeProvider();
        var dto = faker.Get();
        dto.Id = 10;

        var result = await this._sut.AddUser(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(UserErrors.ValidationFailed.FirstError);
    }

    [Fact]
    public async Task Update_WhenUserDtoHasInvalidId_ValidationError()
    {
        var faker = new UserDtoFakeProvider();
        var dto = faker.Get();
        dto.Id = -100;

        var result = await this._sut.UpdateUser(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(UserErrors.ValidationFailed.FirstError);
    }

    [Fact]
    public async Task Update_WhenNotExists_NotFoundError()
    {
        var faker = new UserDtoFakeProvider();
        var dto = faker.Get();

        this._repositoryMock.GetById(Arg.Any<int>()) !.Returns(Task.FromResult<UserEntity>(null!));

        var result = await this._sut.UpdateUser(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(UserErrors.NotFound(dto.Id).FirstError);
    }

    [Theory]
    [ClassData(typeof(UserDtoFakeProvider))]
    public async Task Update_WhenValid_UserUpdated(UserDto dto)
    {
        var faker = new UserDtoFakeProvider();
        var updatedUser = faker.Get();
        updatedUser.Id = dto.Id;

        var oldEnt = new UserEntity
        {
            Id = dto.Id,
            Email = dto.Email,
            Password = dto.Password,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Nickname = dto.Nickname,
        };

        this._repositoryMock.GetById(Arg.Any<int>()).Returns(oldEnt);
        this._repositoryMock.Update(Arg.Any<UserEntity>())
            .Returns(c => Task.FromResult(c.Arg<UserEntity>()));

        var result = await this._sut.UpdateUser(updatedUser);

        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.Password.Should().Be(updatedUser.Password);
        result.Value.Email.Should().Be(updatedUser.Email);
        result.Value.FirstName.Should().Be(updatedUser.FirstName);
        result.Value.LastName.Should().Be(updatedUser.LastName);
        result.Value.Nickname.Should().Be(updatedUser.Nickname);
    }

    [Fact]
    public async Task Get_WhenNotExists_NotFoundError()
    {
        const int id = 1;

        this._repositoryMock.GetById(Arg.Any<int>()) !.Returns(Task.FromResult<UserEntity>(null!));

        var result = await this._sut.GetUserById(id);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(UserErrors.NotFound(id).FirstError);
    }

    [Theory]
    [ClassData(typeof(UserDtoFakeProvider))]
    public async Task Get_WhenExists_UserDtoReturned(UserDto dto)
    {
        var profileEntity = new UserEntity()
        {
            Id = dto.Id,
            Email = dto.Email,
            Password = dto.Password,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Nickname = dto.Nickname,
        };

        this._repositoryMock.GetById(Arg.Any<int>())!.Returns(Task.FromResult(profileEntity));

        var result = await this._sut.GetUserById(dto.Id);

        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.Password.Should().Be(dto.Password);
        result.Value.Email.Should().Be(dto.Email);
        result.Value.FirstName.Should().Be(dto.FirstName);
        result.Value.LastName.Should().Be(dto.LastName);
        result.Value.Nickname.Should().Be(dto.Nickname);
    }
}