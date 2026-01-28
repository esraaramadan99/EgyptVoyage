using EgyptVoyage.Application.DTOs.Review;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Application.Validators;

public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewDtoValidator()
    {
        RuleFor(x => x.EntityId)
            .NotEmpty().WithMessage("Entity ID is required");

        RuleFor(x => x.EntityType)
            .NotEmpty().WithMessage("Entity type is required")
            .Must(BeValidEntityType).WithMessage("Entity type must be Hotel, Restaurant, Landmark, or Program");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment is required")
            .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters");
    }

    private bool BeValidEntityType(string entityType)
    {
        return entityType == "Hotel" || entityType == "Restaurant" ||
               entityType == "Landmark" || entityType == "Program";
    }
}