using Newtonsoft.Json;
using Sample.TodoApp.Auth.Login;

namespace Sample.TodoApp.Auth;

public class User
{
    public string Id { get; }
    public UserName Name { get; }
    public Password Password { get; }
    
    [JsonConstructor]
    public User(string id, UserName name, Password password)
    {
        Id = id;
        Name = name;
        Password = password;
    }

    public string UserId => Id.Replace("Users/", "");

    public bool ValidateLoginCredentials(LoginRequest request) => 
        Name == UserName.From(request.UserName) && Password.Validate(request.InputPassword);
}