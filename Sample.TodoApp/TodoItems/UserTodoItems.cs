using System.Text.Json.Serialization;
using ValueOf;

namespace Sample.TodoApp.TodoItems;

public class UserTodoItems
{
    public string Id { get; }
    public Dictionary<string, TodoItem> Items { get; }
    
    [JsonConstructor]
    public UserTodoItems(string id, Dictionary<string, TodoItem> items)
    {
        Id = id;
        Items = items;
    }

    public static UserTodoItems NewUserTodoItems(UserTodoItemsId id) =>
        new(id.ToString(), new Dictionary<string, TodoItem>());
}

public class UserTodoItemsId : ValueOf<string, UserTodoItemsId>
{
    public new static UserTodoItemsId From(string value) => ValueOf<string, UserTodoItemsId>.From($"UserTodoItems/{value}");
    
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ArgumentException("Value cannot be null or empty");
    }
}