namespace Sample.TodoApp.TodoItems.GetItem;

public class Response
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }


    public Response FromDomain(TodoItem item) => new()
    {
        Id = item.Id.ToString(),
        Title = item.Title.ToString(),
        Description = item.Description.ToString(),
        Status = item.Status,
        CreatedAt = item.CreatedAt
    };
}