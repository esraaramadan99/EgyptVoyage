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

    /*
    public CreateProgramDtoValidator()
    {
        RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Program name is required")
             .MaximumLength(200);

        RuleFor(x => x.Duration.TotalHours)
        .GreaterThan(0)
         .WithMessage("Duration must be greater than 0 hours");


        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .MaximumLength(100);




    }


    */


    public CreateProgramDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Program name is required")
            .MaximumLength(200);

        RuleFor(x => x.DurationValue)
            .GreaterThan(0).WithMessage("Duration must be greater than 0");

        RuleFor(x => x.DurationUnit)
            .NotEmpty().WithMessage("Duration unit is required")
            .Must(u => u.ToLower() == "days" || u.ToLower() == "hours")
            .WithMessage("Duration unit must be 'Days' or 'Hours'");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .MaximumLength(100);
    }
}




