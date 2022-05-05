using FastEndpoints;

namespace Sample.TodoApp.TodoItems.GetItem;

public class GetItemRequest
{
    [FromClaim]
    public string UserId { get; set; }
    
    public string ItemId { get; set; }
}