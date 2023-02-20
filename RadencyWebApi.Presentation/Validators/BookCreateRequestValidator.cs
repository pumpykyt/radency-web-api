using System.Data;
using FluentValidation;
using RadencyWebApi.DataTransfer.Requests;

namespace RadencyWebApi.Presentation.Validators;

public class BookCreateRequestValidator : AbstractValidator<BookCreateRequest>
{
    public BookCreateRequestValidator()
    {
        RuleFor(t => t.Author)
            .NotEmpty()
            .WithMessage("Author field must not be empty");
        
        RuleFor(t => t.Content)
            .NotEmpty()
            .WithMessage("Content field must not be empty");
        
        RuleFor(t => t.Genre)
            .NotEmpty()
            .WithMessage("Genre field must not be empty");
        
        RuleFor(t => t.Title)
            .NotEmpty()
            .WithMessage("Title field must not be empty");
    }
}