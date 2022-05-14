using Newtonsoft.Json;
using ValueOf;

namespace Sample.TodoApp.TodoItems;

public class TodoItem
{
    public TodoItemId Id { get; }
    public Title Title { get; }
    public Description Description { get; }
    public Status Status { get; }
    public DateTime CreatedAt { get; }
    
    [JsonConstructor]
    public TodoItem(TodoItemId id, Title title, Description description, Status status, DateTime createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
    }

    public static TodoItem NewItem(string title, string description) => 
        new(TodoItemId.NewId(), Title.From(title), Description.From(description), Status.Pending, DateTime.UtcNow);
}

public class TodoItemId : ValueOf<string, TodoItemId>
{
    public static TodoItemId NewId() => From(Guid.NewGuid().ToString());
}

public class Title : ValueOf<string, Title> {}
public class Description : ValueOf<string, Description> {}

public enum Status
{ 
    Pending,
    InProcess,
    Completed
}