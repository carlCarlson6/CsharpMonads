namespace Sample.TodoApp.Auth.Login;

public class LoginResponse { }

public class SuccessfulLoginResponse : LoginResponse
{
    public string Token { get; set; }
}

public class ErrorLoginResponse : LoginResponse
{
    public string Message { get; set; }
}