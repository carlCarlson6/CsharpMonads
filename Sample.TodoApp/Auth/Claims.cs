namespace Sample.TodoApp.Auth;

public static class Claims
{
    public static (string claimType, string claimValue) UserNameClaim(UserName name) =>
        ("UserName", name.ToString());
    
    public static (string claimType, string claimValue) UserId(string id) =>
        ("UserId", id);
}