using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastAPI;

[Route("api/weather/")]
[Produces("application/json")]
[ApiController]
public class WeatherController : ControllerBase
{
    private string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }


    [HttpGet("/weatherforecast")]
    [Consumes("application/json")]
    public ActionResult<string[]> GetWeatherForecast()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

        return Ok(forecast);
    }


    [HttpGet("/calculateforecast/{temperatureC}")]
    [Consumes("application/json")]
    public ActionResult<WeatherForecast> CalculateWeatherForecast(int temperatureC)
    {
         var calc = new WeatherForecast(
            DateTime.Now, 
            temperatureC, 
            summaries[Random.Shared.Next(summaries.Length)]
        );

         return Ok(calc);
    }
}