using Monads;
using Raven.Client.Documents;
using Sample.TodoApp.Abstractions;
using Sample.TodoApp.TodoItems.Infrastructure;

namespace Sample.TodoApp.TodoItems.GetItem;

public class TodoItemFinder : IDomainService<GetItemRequest, TodoItem>
{
    private readonly IDocumentStore _store;

    public TodoItemFinder(IDocumentStore store) => _store = store;

    public async Task<Result<TodoItem>> Execute(GetItemRequest input)
    {
        using var session = _store.OpenAsyncSession();
        var userItems = await session.LoadUserTodoItemsAsync(UserTodoItemsId.From(input.UserId));

        userItems.Items.TryGetValue(input.ItemId, out var item);
        return Maybe<TodoItem>.Create(item).ToResult();
    }
}