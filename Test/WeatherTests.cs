using Authors.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

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
