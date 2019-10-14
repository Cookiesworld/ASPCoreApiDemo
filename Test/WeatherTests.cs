using System;
using Xunit;
using Authors.Controllers;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging.Abstractions;

namespace Test
{    
    public class WeatherTests
    {
        private readonly ITestOutputHelper output;

        public WeatherTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetWeatherReturnsData()
        {
            var logger = (new NullLoggerFactory()).CreateLogger<WeatherForecastController>();
            var controller = new WeatherForecastController(logger);
            var result = controller.Get();
            Assert.NotNull(result);
        }
    }
}
