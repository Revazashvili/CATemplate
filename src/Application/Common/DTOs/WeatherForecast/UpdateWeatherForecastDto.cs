using System;
using Application.Common.SwaggerSchemaFilters.WeatherForecasts;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Common.DTOs.WeatherForecast
{
    [SwaggerSchemaFilter(typeof(UpdateWeatherForecastDtoSchemaFilter))]
    [SwaggerSchema(Required = new[] { "Id","Date","TemperatureC","Summary" })]
    public class UpdateWeatherForecastDto
    {
        [SwaggerSchema("The Weather Forecast Id")]
        public long Id { get; set; }
        [SwaggerSchema("Date When Weather Is Predicted",Format = "date")]
        public DateTime Date { get; set; }
        [SwaggerSchema("Temperature In Celsius",Title = "Some Title")]
        public int TemperatureC { get; set; }
        [SwaggerSchema("Some Information About This Weather")]
        public string Summary { get; set; }
    }
}