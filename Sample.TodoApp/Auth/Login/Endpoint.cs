using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Monads;
using Raven.Client.Documents;
using Sample.TodoApp.Auth.Infrastructure;
using Sample.TodoApp.Infrastructure;

namespace Sample.TodoApp.Auth.Login;

[HttpPost(ApiUris.Login), AllowAnonymous]
public class Endpoint : Endpoint<LoginRequest, LoginResponse>
{
    private const string SigningKey = ")H@McQfTjWnZr4u7x!z%C*F-JaNdRgUkXp2s5v8y/B?D(G+KbPeShVmYq3t6w9z$"; // TODO move to config
    private readonly IDocumentStore _store;
    public Endpoint(IDocumentStore store) => _store = store;
    
    private readonly Result<User> _invalidCredentialsResult = Result<User>.Ko("The supplied credentials are invalid!");

    public override async Task HandleAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var maybeUser = await _store.LoadMaybeUser(UserName.From(loginRequest.UserName));
        var resultUser = maybeUser.Map(
            user => user.ValidateLoginCredentials(loginRequest) 
                ? Result<User>.Ok(user)
                : _invalidCredentialsResult,
            () => _invalidCredentialsResult);
        
        var response = resultUser.Map<User, LoginResponse>(
            user => SuccessfulLoginResponse.Create(user, SigningKey), 
            error => new ErrorLoginResponse{ Message = error.Message });

        Func<Task> sendResponse = response switch
        {
            ErrorLoginResponse errorResponse => () => SendAsync(errorResponse, 400, cancellationToken),
            SuccessfulLoginResponse successfulResponse => () => SendAsync(successfulResponse, cancellation: cancellationToken),
            _ => () => SendErrorsAsync(cancellation: cancellationToken)
        };
        await sendResponse();
    }
}