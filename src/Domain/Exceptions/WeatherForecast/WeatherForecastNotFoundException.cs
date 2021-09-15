using System;

namespace Domain.Exceptions.WeatherForecast
{
    public class WeatherForecastNotFoundException : Exception
    {
        public WeatherForecastNotFoundException() : base("Weather forecast could not be found.")
        {
        }
    }
}