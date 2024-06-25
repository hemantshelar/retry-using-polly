using Microsoft.AspNetCore.Mvc;
using Polly;

namespace retry_using_polly.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class WeatherForecastController : ControllerBase
	{

		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly MathsService _mathsService;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, MathsService mathsService)
		{
			_logger = logger;
			_mathsService = mathsService;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}



		[HttpGet]
		public async Task<IActionResult> GetMathsServiceResult()
		{

			IAsyncPolicy<int> policy = Policy<int>
				.Handle<Exception>()
				.RetryAsync(5);

			var result = await policy.ExecuteAsync(async () =>
			{
				int result = await _mathsService.DoOperation();
				return result;

			});
			return Ok(result);

		}
	}
}
