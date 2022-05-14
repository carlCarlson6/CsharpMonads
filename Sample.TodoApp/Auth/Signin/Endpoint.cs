using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Monads;
using Raven.Client.Documents;
using Sample.TodoApp.Auth.Infrastructure;
using Sample.TodoApp.Infrastructure;

// ReSharper disable once UnusedType.Global
namespace Sample.TodoApp.Auth.Signin;

[HttpPost(ApiUris.Signin), AllowAnonymous]
public class Endpoint : Endpoint<SigninRequest, SigninResponse>
{
    private readonly IDocumentStore _documentStore;
    private readonly JwtGenerator _jwtGenerator;

    public Endpoint(IDocumentStore documentStore, JwtGenerator jwtGenerator)
    {
        _documentStore = documentStore;
        _jwtGenerator = jwtGenerator;
    }

    public override async Task HandleAsync(SigninRequest request, CancellationToken cancellationToken)
    {
        using var session = _documentStore.OpenAsyncSession();
        var maybeUser = await session.LoadMaybeUser(UserName.From(request.UserName));
        var result = maybeUser.Map(
            _ => Result<User>.Ko(new Error("user name already take")),
            () => Result<User>.Ok(new User(UserId.NewId().ToString(), UserName.From(request.UserName), Password.From(request.Password))));
        
        throw new NotImplementedException();
    }
}