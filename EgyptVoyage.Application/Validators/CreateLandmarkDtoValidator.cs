using EgyptVoyage.Application.DTOs.Landmark;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EgyptVoyage.Application.Validators;

public class CreateLandmarkDtoValidator : AbstractValidator<CreateLandmarkDto>
{
    public CreateLandmarkDtoValidator()
    {
        RuleFor(x => x.LandmarkName)
            .NotEmpty().WithMessage("Landmark name is required")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Location)
            .NotNull().WithMessage("Location is required");

        RuleFor(x => x.Price)
           .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");
    }

}
