namespace BarBreak.Infrastructure.Repositories
{
    using BarBreak.Core.User;

    // Focus less on the infrastructure level according to DDD than on the domain model itself
    // you shouldn't be dependent on how-to's and what-to's, make a unified solution and only support additional requirements via the right interface
    // uou are using Repository from DDD, but you arn't doing it correctly, as if it were a Mediator
    // for more information - Eric Evans DDD: Tackling Complexity in the Heart of Software. You must feel that HEART of Software
    public class UserRepository(ApplicationDbContext dbContext)
        : RepositoryBase<int, UserEntity, ApplicationDbContext>(dbContext),
            IUserRepository;
}
