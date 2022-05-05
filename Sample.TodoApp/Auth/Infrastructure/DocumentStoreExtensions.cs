using Monads;
using Raven.Client.Documents;
using Sample.TodoApp.Infrastructure.RavenDb.Indexes;

namespace Sample.TodoApp.Auth.Infrastructure;

public static class DocumentStoreExtensions
{
    public static async Task<Maybe<User>> LoadMaybeUser(this IDocumentStore store, UserName name)
    {
        using var session = store.OpenAsyncSession();
        var user = await session
            .Query<Users_ByName.Result, Users_ByName>()
            .Where(u => u.Name == name)
            .OfType<User>()
            .FirstOrDefaultAsync();

        return user is null ? Maybe<User>.None() : Maybe<User>.Some(user);
    }
}