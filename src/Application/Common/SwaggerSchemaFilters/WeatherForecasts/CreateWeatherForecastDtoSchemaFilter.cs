using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Common.SwaggerSchemaFilters.WeatherForecasts
{
    public class CreateWeatherForecastDtoSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Example = new OpenApiObject
            {
                ["Date"] = new OpenApiDateTime(DateTime.Now),
                ["TemperatureC"] = new OpenApiInteger(10),
                ["Summary"] = new OpenApiString("Some Information About This Weather")
            };
        }
    }
}