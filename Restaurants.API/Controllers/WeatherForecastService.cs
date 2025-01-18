namespace Restaurants.API.Controllers
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int numberOfResults, int minTempDeg, int maxTempDeg);
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public IEnumerable<WeatherForecast> Get(int numberOfResults, int minTempDeg, int maxTempDeg)
        {
            return Enumerable.Range(1, numberOfResults).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(minTempDeg, maxTempDeg),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
            
        }
    }
}
