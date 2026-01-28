using EgyptVoyage.Application.DTOs.Program;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Application.Validators;

public class CreateProgramDtoValidator : AbstractValidator<CreateProgramDto>
{
    public CreateProgramDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Program name is required")
            .MaximumLength(200);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Location)
            .NotNull().WithMessage("Location is required");
    }
}