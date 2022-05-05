using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Sample.TodoApp.Infrastructure;

namespace Sample.TodoApp.Health;

[HttpGet(ApiUris.Health), AllowAnonymous]
public class HealthEndpoint : EndpointWithoutRequest<Response>
{
    public override Task HandleAsync(CancellationToken ct) => SendAsync(new Response(), cancellation: ct);
}

public class Response
{
    public string Message { get; set; } = "hello world! :) - i'm healthy";
}