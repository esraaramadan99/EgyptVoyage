using EgyptVoyage.Application.DTOs.Restaurant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Application.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.RestaurantName)
            .NotEmpty().WithMessage("Restaurant name is required")
            .MaximumLength(200);

        RuleFor(x => x.CuisineType)
            .NotEmpty().WithMessage("Cuisine type is required");

        RuleFor(x => x.Location)
            .NotNull().WithMessage("Location is required");
    }
}