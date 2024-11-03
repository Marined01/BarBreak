namespace BarBreak.Core.Tests.Role;

using BarBreak.Core.Role;
using BarBreak.Core.Tests.Role.Helpers;

public class RoleServiceTests
{
    private readonly IRoleService _sut;
    private readonly IRoleRepository _repositoryMock;

    public RoleServiceTests()
    {
        this._repositoryMock = Substitute.For<IRoleRepository>();
        this._sut = new RoleService(this._repositoryMock, Substitute.For<Serilog.ILogger>());
    }

    [Fact]
    public async Task Create_WhenRoleDtoHasId_ValidationError()
    {
        var faker = new RoleDtoFakeProvider();
        var dto = faker.Get();
        dto.Id = 10;

        var result = await this._sut.AddRole(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(RoleErrors.ValidationFailed.FirstError);
    }

    [Fact]
    public async Task Update_WhenRoleDtoHasInvalidId_ValidationError()
    {
        var faker = new RoleDtoFakeProvider();
        var dto = faker.Get();
        dto.Id = -100;

        var result = await this._sut.UpdateRole(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(RoleErrors.ValidationFailed.FirstError);
    }

    [Fact]
    public async Task Update_WhenNotExists_NotFoundError()
    {
        var faker = new RoleDtoFakeProvider();
        var dto = faker.Get();

        this._repositoryMock.GetById(Arg.Any<int>()) !.Returns(Task.FromResult<RoleEntity>(null!));

        var result = await this._sut.UpdateRole(dto);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(RoleErrors.NotFound(dto.Id).FirstError);
    }

    [Theory]
    [ClassData(typeof(RoleDtoFakeProvider))]
    public async Task Update_WhenValid_RoleUpdated(RoleDto dto)
    {
        var faker = new RoleDtoFakeProvider();
        var updatedRole = faker.Get();
        updatedRole.Id = dto.Id;

        var oldEnt = new RoleEntity
        {
            Id = dto.Id,
            RoleName = dto.RoleName,
        };

        this._repositoryMock.GetById(Arg.Any<int>()).Returns(oldEnt);
        this._repositoryMock.Update(Arg.Any<RoleEntity>())
            .Returns(c => Task.FromResult(c.Arg<RoleEntity>()));

        var result = await this._sut.UpdateRole(updatedRole);

        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.RoleName.Should().Be(updatedRole.RoleName);
    }

    [Fact]
    public async Task Get_WhenNotExists_NotFoundError()
    {
        const int id = 1;

        this._repositoryMock.GetById(Arg.Any<int>())!.Returns(Task.FromResult<RoleEntity>(null!));

        var result = await this._sut.GetRoleById(id);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(RoleErrors.NotFound(id).FirstError);
    }

    [Theory]
    [ClassData(typeof(RoleDtoFakeProvider))]
    public async Task Get_WhenExists_RoleDtoReturned(RoleDto dto)
    {
        var profileEntity = new RoleEntity()
        {
            Id = dto.Id,
            RoleName = dto.RoleName,
        };

        this._repositoryMock.GetById(Arg.Any<int>())!.Returns(Task.FromResult(profileEntity));

        var result = await this._sut.GetRoleById(dto.Id);

        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.RoleName.Should().Be(dto.RoleName);
    }
}