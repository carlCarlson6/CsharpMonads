using FastEndpoints;
using FluentValidation;

namespace Sample.TodoApp.TodoItems.CreateItem;

public class CreateItemRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    [FromClaim]
    public string UserId { get; set; }
}

// ReSharper disable once UnusedType.Global
public class Validator : Validator<CreateItemRequest>
{
    public Validator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("title is mandatory");
    }
}