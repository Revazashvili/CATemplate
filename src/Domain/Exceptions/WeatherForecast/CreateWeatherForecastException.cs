using System;

namespace Domain.Exceptions.WeatherForecast
{
    public class CreateWeatherForecastException : Exception
    {
        public CreateWeatherForecastException() : base("Can't create new weather forecast.")
        {
        }
    }
}