using Monads;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Sample.TodoApp.Abstractions;
using Sample.TodoApp.TodoItems.Infrastructure;

namespace Sample.TodoApp.TodoItems.CreateItem;

public class UserTodoItemsCreator : IDomainService<CreateItemRequest, TodoItemId>
{
    private readonly IDocumentStore _store;

    public UserTodoItemsCreator(IDocumentStore store) => _store = store;
    
    public async Task<Result<TodoItemId>> Execute(CreateItemRequest input)
    {
        using var session = _store.OpenAsyncSession();
        var todoItems = await session.LoadUserTodoItemsAsync(UserTodoItemsId.From(input.UserId));

        var newItem = TodoItem.NewItem(input.Title, input.Description);
        
        return todoItems.Items.TryAdd(newItem.Id.ToString(), newItem) switch
        {
            true => await OnSuccess(newItem.Id, session),
            false => Result<TodoItemId>.Ko(new Error())
        };
    }

    private static async Task<Result<TodoItemId>> OnSuccess(TodoItemId todoItemId, IAsyncDocumentSession session)
    {
        await session.SaveChangesAsync();
        return Result<TodoItemId>.Ok(todoItemId);
    }
}