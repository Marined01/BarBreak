namespace BarBreak.Core.Tests.User.Helpers;

using BarBreak.Core.User;
using BarBreak.Core.Tests.Helpers;

public class UserDtoFakeProvider() : ObjectFakeProviderBase<UserDto>(5)
{
    protected override Func<Faker<UserDto>> DefaultFactory =>
        () => new Faker<UserDto>()
            .RuleFor(x => x.Id, f => f.IndexFaker + 1)
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Password, f => f.Person.Phone)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Username, f => f.Name.FullName());
}