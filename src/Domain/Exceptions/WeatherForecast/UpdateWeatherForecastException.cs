using System;

namespace Domain.Exceptions.WeatherForecast
{
    public class UpdateWeatherForecastException : Exception
    {
        public UpdateWeatherForecastException() : base("Can't update weather forecast.")
        {
        }
    }
}