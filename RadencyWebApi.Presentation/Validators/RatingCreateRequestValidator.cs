using FluentValidation;
using RadencyWebApi.DataTransfer.Requests;

namespace RadencyWebApi.Presentation.Validators;

public class RatingCreateRequestValidator : AbstractValidator<RatingCreateRequest>
{
    public RatingCreateRequestValidator()
    {
        RuleFor(t => t.Score)
            .NotEmpty()
            .WithMessage("Score must not be empty")
            .InclusiveBetween(1, 5)
            .WithMessage("Score value must be in range: [1, 5]");
    }
}