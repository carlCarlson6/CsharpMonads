namespace Sample.TodoApp.TodoItems.CreateItem;

public class Response
{
    public string Message { get; } = "Item created successfully";
}

public class RoutesValueResponse
{
    public string ItemId { get; set; }
    
    public RoutesValueResponse(string itemId)
    {
        ItemId = itemId;
    }
}