using FastEndpoints.Security;
using static Sample.TodoApp.Auth.Claims;

namespace Sample.TodoApp.Auth.Login;

public class LoginResponse { }

public class SuccessfulLoginResponse : LoginResponse
{
    public string Token { get; set; }

    public static SuccessfulLoginResponse Create(User user, string signingKey) => new()
    {
        Token = JWTBearer.CreateToken(
            signingKey,
            DateTime.UtcNow.AddDays(1),
            claims: new[] { UserNameClaim(user.Name), UserId(user.UserId) })
    };
}

public class ErrorLoginResponse : LoginResponse
{
    public string Message { get; set; }
}