using AutoMapper;
using Echo.Domain.Shared.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMapper mapper;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper)
    {
        _logger = logger;
        this.mapper = mapper;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        try
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

        }
        catch (Exception e)
        {

            throw;
        }
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("/getdata/{NumberOfDataSet}")]
    public async Task<IActionResult> getdatasat(int NumberOfDataSet)
    {

        try
        {
            PopulateDatahandler populateDatahandler = new PopulateDatahandler();
            if (populateDatahandler is null)
            {
                return Problem("the object is null");
            }
            var data = populateDatahandler.GetRandomData(NumberOfDataSet);
            return Ok(data);

        }
        catch (Exception e)
        {

            return Problem(e.Message); ;
        }
    }

    [HttpGet("/getdatamessage/{NumberOfDataSet}")]
    public async Task<IActionResult> getdatamessage(int NumberOfDataSet)
    {

        try
        {
            PopulateDatahandler populateDatahandler = new PopulateDatahandler();
            if (populateDatahandler is null)
            {
                return Problem("the object is null");
            }
            var data = populateDatahandler.GetRandomDateForSendMessageRequest(NumberOfDataSet);
            return Ok(data);

        }
        catch (Exception e)
        {

            return Problem(e.Message); ;
        }
    }
}
