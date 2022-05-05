using ValueOf;
using System.Web.Helpers;

namespace Sample.TodoApp.Auth;

public class Password : ValueOf<string, Password>
{
    protected override void Validate()
    {
        ValidatePasswordFormat(Value);
        Value = Crypto.HashPassword(Value);
    }

    private static void ValidatePasswordFormat(string value)
    {
        // TODO - add more validations
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or empty");
    }

    public bool Validate(string inputPassword) => Crypto.VerifyHashedPassword(Value, inputPassword);
}