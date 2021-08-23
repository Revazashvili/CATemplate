using System;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Common.DTOs.WeatherForecast
{
    public class GetWeatherForecastDto
    {
        public GetWeatherForecastDto(long id, DateTime date, int temperatureC, string summary)
        {
            Id = id;
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }

        [SwaggerSchema("The Weather Forecast Id")]
        public long Id { get; set; }
        [SwaggerSchema("Date When Weather Is Predicted",Format = "date")]
        public DateTime Date { get; set; }
        [SwaggerSchema("Temperature In Celsius",Title = "Some Title")]
        public int TemperatureC { get; set; }
        [SwaggerSchema("Temperature In Fahrenheit",Title = "Some Title")]
        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
        [SwaggerSchema("Some Information About This Weather")]
        public string Summary { get; set; }
    }
}