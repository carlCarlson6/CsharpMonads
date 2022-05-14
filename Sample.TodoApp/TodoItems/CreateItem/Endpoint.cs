using FastEndpoints;
using Monads;
using Sample.TodoApp.Abstractions;
using Sample.TodoApp.Infrastructure;

namespace Sample.TodoApp.TodoItems.CreateItem;
// ReSharper disable once UnusedType.Global

[HttpPost(ApiUris.TodoItem)]
public class Endpoint : Endpoint<CreateItemRequest, CreateItemResponse>
{
    private readonly IDomainService<CreateItemRequest, TodoItemId> _service;

    public Endpoint(IDomainService<CreateItemRequest, TodoItemId> service) => _service = service;

    public override async Task HandleAsync(CreateItemRequest createItemRequest, CancellationToken cancellationToken)
    {
        var result = await _service.Execute(createItemRequest);
        await HandleEndpointResult(result);
    }

    private Task HandleEndpointResult(Result<TodoItemId> result) => result.Map(OnOkResult, OnKoResult);
    private Task OnOkResult(TodoItemId id) => 
        SendCreatedAtAsync<GetItem.Endpoint>(new RoutesValueResponse(id.ToString()), new CreateItemResponse());
    private Task OnKoResult(Error _) => SendErrorsAsync();
}