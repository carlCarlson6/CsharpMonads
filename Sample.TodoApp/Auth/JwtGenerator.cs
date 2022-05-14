using FastEndpoints.Security;
using static Sample.TodoApp.Auth.Claims;

namespace Sample.TodoApp.Auth;

public class JwtGenerator
{
    private string SigningKey = ")H@McQfTjWnZr4u7x!z%C*F-JaNdRgUkXp2s5v8y/B?D(G+KbPeShVmYq3t6w9z$"; // TODO move to config
    
    public string Generate(User user) => 
        JWTBearer.CreateToken(
            SigningKey,
            DateTime.UtcNow.AddDays(1),
            claims: new[] { UserNameClaim(user.Name), UserId(user.UserId) });
}