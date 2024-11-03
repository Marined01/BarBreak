namespace BarBreak.Core.Tests.Role.Helpers;

using BarBreak.Core.Role;
using BarBreak.Core.Tests.Helpers;

public class RoleDtoFakeProvider() : ObjectFakeProviderBase<RoleDto>(5)
{
    protected override Func<Faker<RoleDto>> DefaultFactory =>
        () => new Faker<RoleDto>()
            .RuleFor(x => x.Id, f => f.IndexFaker + 1)
            .RuleFor(x => x.RoleName, f => f.Name.JobTitle());
}