namespace BarBreak.Core.Tests.Helpers;

using System.Collections;

public abstract class ObjectFakeProviderBase<TModel>(int amount) : IEnumerable<object[]>
    where TModel : class
{
    protected virtual int FakesNumber { get; set; } = amount;

    protected virtual Func<Faker<TModel>> DefaultFactory => () => throw new NotImplementedException();

    internal TModel Get()
    {
        ArgumentNullException.ThrowIfNull(this.DefaultFactory);

        var faker = this.DefaultFactory();
        return faker.Generate();
    }

    protected virtual IEnumerable<TModel> GetFakes(
        Func<Faker<TModel>> fakerFactory)
    {
        fakerFactory ??= this.DefaultFactory;
        ArgumentNullException.ThrowIfNull(fakerFactory);

        var faker = fakerFactory();

        foreach (var fake in faker.Generate(FakesNumber))
        {
            yield return fake;
        }
    }

    public IEnumerator<object[]> GetEnumerator()
        => this.GetFakes(this.DefaultFactory)
            .Select(fake => (object[])[fake])
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}