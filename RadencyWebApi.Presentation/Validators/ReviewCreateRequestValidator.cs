using FluentValidation;
using RadencyWebApi.DataTransfer.Requests;

namespace RadencyWebApi.Presentation.Validators;

public class ReviewCreateRequestValidator : AbstractValidator<ReviewCreateRequest>
{
    public ReviewCreateRequestValidator()
    {
        RuleFor(t => t.Message)
            .NotEmpty()
            .WithMessage("Message field must not be empty");
        
        RuleFor(t => t.Reviewer)
            .NotEmpty()
            .WithMessage("Reviewer field must not be empty");
    }
}