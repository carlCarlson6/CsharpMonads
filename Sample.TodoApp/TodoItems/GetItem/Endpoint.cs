using FastEndpoints;
using Monads;
using Sample.TodoApp.Abstractions;
using Sample.TodoApp.Infrastructure;

namespace Sample.TodoApp.TodoItems.GetItem;

[HttpGet(ApiUris.TodoItemId)]
public class Endpoint : Endpoint<GetItemRequest, Response>
{
    private readonly IDomainService<GetItemRequest, TodoItem> _service;

    public Endpoint(IDomainService<GetItemRequest, TodoItem> service) => _service = service;

    public override async Task HandleAsync(GetItemRequest getItemRequest, CancellationToken ct)
    {
        var result = await _service.Execute(getItemRequest);
        await HandleEndpointResult(result);
    }
    
    private Task HandleEndpointResult(Result<TodoItem> result) => result.Map(OnOkResult, OnKoResult);
    private Task OnOkResult(TodoItem item) => SendAsync(Response.FromDomain(item));
    private Task OnKoResult(Error error) => SendNotFoundAsync();
}