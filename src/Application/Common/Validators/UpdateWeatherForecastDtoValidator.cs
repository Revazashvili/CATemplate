using System;
using Application.Common.DTOs.WeatherForecast;
using FluentValidation;

namespace Application.Common.Validators
{
    public class UpdateWeatherForecastDtoValidator : AbstractValidator<UpdateWeatherForecastDto>
    {
        public UpdateWeatherForecastDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty().WithMessage("Id must be provided");
            
            RuleFor(x => x.TemperatureC)
                .NotNull().NotEmpty().WithMessage("TemperatureC must be provided");

            RuleFor(x => x.Date)
                .NotEmpty().NotNull().WithMessage("Date must be provided")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Date must be greater that today");

            RuleFor(x => x.Summary)
                .NotNull().NotEmpty().WithMessage("Summary must be provided");
        }
    }
}