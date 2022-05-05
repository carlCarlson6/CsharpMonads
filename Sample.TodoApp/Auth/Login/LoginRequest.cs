using FastEndpoints;
using FluentValidation;

namespace Sample.TodoApp.Auth.Login;

public class LoginRequest
{
    public string UserName { get; set; }
    public string InputPassword { get; set; }
}

public class Validator : Validator<LoginRequest>
{
    public Validator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("username is required")
            .MinimumLength(3).WithMessage("username is too short!")
            .MaximumLength(15).WithMessage("username is too long!");

        
        RuleFor(x => x.InputPassword)
            .NotEmpty().WithMessage("a password is required!")
            .MinimumLength(10).WithMessage("password is too short!")
            .MaximumLength(25).WithMessage("password is too long!");
    }
}