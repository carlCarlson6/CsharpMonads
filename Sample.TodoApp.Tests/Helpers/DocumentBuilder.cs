using System.Threading.Tasks;
using Raven.Client.Documents;

namespace Sample.TodoApp.Tests.Helpers;

public abstract class DocumentBuilder<T> where T : class
{
    internal async Task<T> BuildAsync(IDocumentStore documentStore, T document)
    {
        using var session = documentStore.OpenAsyncSession();
        await session.StoreAsync(document);
        await session.SaveChangesAsync();
        return document;
    }
    
    internal T Build(T document)
    {
        return document;
    }
}