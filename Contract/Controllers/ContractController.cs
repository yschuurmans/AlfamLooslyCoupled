using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contract.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly ILogger<ContractController> _logger;
        public RabbitMQService _publisher { get; set; }

        public ContractController(ILogger<ContractController> logger, RabbitMQService publisher)
        {
            _logger = logger;
            _publisher = publisher;

            //_publisher.RegisterConsumer<WeatherForecast>((weatherForecast) =>
            //{
            //    Console.WriteLine($"Received: {weatherForecast}");
            //});
            //_publisher.RegisterConsumer<NotAWeatherForecast>((weatherForecast) =>
            //{
            //    Console.WriteLine($"Received: {weatherForecast}");
            //});
        }

        [HttpGet("WeatherForecast")]
        public string GetWeatherForecast()
        {
            _publisher.PublishObject(new WeatherForecast() {Date = DateTime.Now, Summary = "Does this work?", TemperatureC = 30});

            return "OK";
        }
        [HttpGet("NotAWeatherForecast")]
        public string GetNotAWeatherForecast()
        {
            _publisher.PublishObject(new NotAWeatherForecast() { Date = DateTime.Now, Summary = "Does this work?", SomethingElse = "Testing 2" });

            return "OK";
        }
    }
}
