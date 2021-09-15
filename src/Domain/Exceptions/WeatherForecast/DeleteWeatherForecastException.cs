using System;

namespace Domain.Exceptions.WeatherForecast
{
    public class DeleteWeatherForecastException : Exception
    {
        public DeleteWeatherForecastException() : base("Error occurred while deleting weather forecast.")
        {
        }
    }
}