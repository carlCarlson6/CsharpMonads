using Raven.Client.Documents.Session;
using static Sample.TodoApp.TodoItems.UserTodoItems;

namespace Sample.TodoApp.TodoItems.Infrastructure;

public static class DocumentStoreExtensions
{
    public static async Task<UserTodoItems> LoadUserTodoItemsAsync(this IAsyncDocumentSession session, UserTodoItemsId userId)
    {
        var todoItems = await session.LoadAsync<UserTodoItems>(userId.ToString());
        if (todoItems is not null) return todoItems;
        
        todoItems = NewUserTodoItems(userId);
        await session.StoreAsync(todoItems);

        return todoItems;
    }
}