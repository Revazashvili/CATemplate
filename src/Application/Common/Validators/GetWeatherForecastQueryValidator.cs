using Application.Queries.WeatherForecasts;
using FluentValidation;

namespace Application.Common.Validators
{
    public class GetWeatherForecastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
    {
        public GetWeatherForecastQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();
        }
    }
}