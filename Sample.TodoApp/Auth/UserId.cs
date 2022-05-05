using ValueOf;

namespace Sample.TodoApp.Auth;

public class UserId : ValueOf<string, UserId>
{
    public static UserId NewId() => ValueOf<string, UserId>.From($"Users/{Guid.NewGuid()}");

    public new static UserId From(string value) => ValueOf<string, UserId>.From($"Users/{value}");
    
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ArgumentException("Value cannot be null or empty");
    }
}