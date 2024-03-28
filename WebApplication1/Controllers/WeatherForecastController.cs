using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class GetCandidatesController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public GetCandidatesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("CheckInterviews")]
        public async Task<IActionResult> GetInterviewCountOnDate([FromBody] DateTime targetDate)
        {
            try
            {
                // Call's the external service to retrieve interviews for the next 14 days
                HttpResponseMessage response = await _httpClient.GetAsync("https://cmricandidates.azurewebsites.net/api/getcandidates");
                response.EnsureSuccessStatusCode();
                List<Interview> interviews = await response.Content.ReadFromJsonAsync<List<Interview>>();

                // Filter interviews for the target date
                var interviewsOnTargetDate = interviews.Where(interview => interview.dateOfInterview.Date == targetDate.Date).ToList();
                StringBuilder builder = new StringBuilder();
                foreach (var interview in interviewsOnTargetDate)
                {
                    builder.AppendLine(interview.ToString()); // Assuming interview has a meaningful ToString() method
                }
                string result = builder.ToString();

                // Return the count as response

                return Ok(new
                {
                    numberOfInterviews = interviewsOnTargetDate.Count,
                    list = result
                });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, $"Failed to retrieve candidates data from the external service: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

public class Interview
{
    public DateTime dateOfInterview { get; set; }
    public string name { get; set; }

        public override string ToString()
        {
            return $"Date: {dateOfInterview}, Name: {name}";
        }
    }

}