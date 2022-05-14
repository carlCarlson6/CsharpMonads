using Monads;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Sample.TodoApp.Infrastructure.RavenDb.Indexes;

namespace Sample.TodoApp.Auth.Infrastructure;

public static class DocumentStoreExtensions
{
    public static Task<Maybe<User>> LoadMaybeUser(this IDocumentStore store, UserName name)
    {
        using var session = store.OpenAsyncSession();
        return session.LoadMaybeUser(name);
    }

    public static async Task<Maybe<User>> LoadMaybeUser(this IAsyncDocumentSession session, UserName name) =>
        await session
                .Query<Users_ByName.Result, Users_ByName>()
                .Where(u => u.Name == name)
                .OfType<User>()
                .FirstOrDefaultAsync() 
            switch
            {
                null => Maybe<User>.None(),
                {} user => Maybe<User>.Some(user)
            };
}