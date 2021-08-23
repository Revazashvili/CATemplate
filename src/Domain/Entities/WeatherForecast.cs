using System;
using Domain.Common;

namespace Domain.Entities
{
    public class WeatherForecast : Auditable
    {
        public WeatherForecast(DateTime date, int temperatureC, string summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }

        /// <summary>
        /// Gets or sets the primary key for this entity.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Gets or sets the weather forecast date.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets Temperature in Celsius
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// Gets Temperature in Fahrenheit
        /// </summary>
        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
        /// <summary>
        /// Gets or sets summary of weather foreacast
        /// </summary>
        public string Summary { get; set; }
    }
}