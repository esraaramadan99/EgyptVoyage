using EgyptVoyage.Application.DTOs.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FluentValidation;

namespace EgyptVoyage.Application.Validators;

public class CreateHotelDtoValidator : AbstractValidator<CreateHotelDto>
{
    public CreateHotelDtoValidator()
    {
        RuleFor(x => x.HotelName)
            .NotEmpty().WithMessage("Hotel name is required")
            .MaximumLength(200);

        RuleFor(x => x.Level)
            .InclusiveBetween(1, 5).WithMessage("Level must be between 1 and 5");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Location)
            .NotNull().WithMessage("Location is required");
    }
}