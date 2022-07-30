using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using wild_lou_weather_api.Models;

namespace wild_lou_weather_api.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private static WeatherResponse _weather { get; set; } = new WeatherResponse();
        private static OneCallResponse _onecall { get; set; } = new OneCallResponse();
        private static ForecastResponse _forecast { get; set; } = new ForecastResponse();

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }

        [HttpGet("weather")]
        public async Task<WeatherResponse> Get(
            [FromQuery] string units, 
            [FromQuery] string lang, 
            [FromQuery] string appid, 
            [FromQuery] string lon, 
            [FromQuery] string lat
        )
        {
            try
            {
                if (_weather.timestampUTC == DateTime.MinValue)
                {
                    await FetchWeather(appid, lat, lon, lang, units);
                } 
                else
                {
                    var timeSpan = DateTime.UtcNow.Subtract(_weather.timestampUTC);
                    if (timeSpan.Hours > 1)
                    {
                        await FetchWeather(appid, lat, lon, lang, units);
                    }
                }

                return _weather;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR => " + ex.Message);
                return new WeatherResponse();
            }
        }

        [HttpGet("onecall")]
        public async Task<OneCallResponse> GetOneCall(
            [FromQuery] string units,
            [FromQuery] string lang,
            [FromQuery] string appid,
            [FromQuery] string lon,
            [FromQuery] string lat,
            [FromQuery] string exclude
        )
        {
            try
            {
                if (_onecall.timestampUTC == DateTime.MinValue)
                {
                    await FetchOneCall(appid, lat, lon, lang, units, exclude);
                }
                else
                {
                    var timeSpan = DateTime.UtcNow.Subtract(_onecall.timestampUTC);
                    if (timeSpan.Minutes > 2)
                    {
                        await FetchOneCall(appid, lat, lon, lang, units, exclude);
                    }
                }

                return _onecall;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR => " + ex.Message);
                return new OneCallResponse();
            }
        }

        [HttpGet("forecast")]
        public async Task<ForecastResponse> GetForecast(
            [FromQuery] string units,
            [FromQuery] string lang,
            [FromQuery] string appid,
            [FromQuery] string lon,
            [FromQuery] string lat
        )
        {
            try
            {
                if (_forecast.timestampUTC == DateTime.MinValue)
                {
                    await FetchForecast(appid, lat, lon, lang, units);
                }
                else
                {
                    var timeSpan = DateTime.UtcNow.Subtract(_forecast.timestampUTC);
                    if (timeSpan.Hours > 1)
                    {
                        await FetchForecast(appid, lat, lon, lang, units);
                    }
                }

                return _forecast;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR => " + ex.Message);
                return new ForecastResponse();
            }
        }

        private async Task FetchWeather(string appid, string lat, string lon, string lang, string units)
        {
            var url = GetOpenWeatherUrl("weather", appid, lat, lon, lang, units, null);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                var weatherAsString = await response.Content.ReadAsStringAsync();
                _weather = JsonConvert.DeserializeObject<WeatherResponse>(weatherAsString);
                if (_weather == null)
                {
                    throw new Exception("_weather is null");
                }
            }

            _weather.timestampUTC = DateTimeOffset.FromUnixTimeSeconds(_weather.dt).DateTime;
            _weather.sys.sunriseUTC = DateTimeOffset.FromUnixTimeSeconds(_weather.sys.sunrise).DateTime;
            _weather.sys.sunsetUTC = DateTimeOffset.FromUnixTimeSeconds(_weather.sys.sunset).DateTime;
        }

        private async Task FetchOneCall(string appid, string lat, string lon, string lang, string units, string exclude)
        {
            var url = GetOpenWeatherUrl("onecall", appid, lat, lon, lang, units, exclude);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                var weatherAsString = await response.Content.ReadAsStringAsync();
                _onecall = JsonConvert.DeserializeObject<OneCallResponse>(weatherAsString);
                if (_onecall == null)
                {
                    throw new Exception("_onecall is null");
                }
            }

            _onecall.timestampUTC = DateTime.UtcNow;
        }

        private async Task FetchForecast(string appid, string lat, string lon, string lang, string units)
        {
            var url = GetOpenWeatherUrl("forecast", appid, lat, lon, lang, units, null);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                var weatherAsString = await response.Content.ReadAsStringAsync();
                _forecast = JsonConvert.DeserializeObject<ForecastResponse>(weatherAsString);
                if (_forecast == null)
                {
                    throw new Exception("_forecast is null");
                }
            }

            _forecast.timestampUTC = DateTime.UtcNow;
        }

        private string GetOpenWeatherUrl(string apiCall, string appid, string lat, string lon, string lang, string units, string exclude)
        {
            if (appid == null) throw new Exception("API Key (appid) is missing. Please provide a valid OpenWeatherMap API Key.");
            if (lat == null) throw new Exception("Latitudinal Coordinate is missing.");
            if (lon == null) throw new Exception("Longitudinal Coordinate is missing.");
            if (units == null) units = "imperial";
            if (exclude == null)
            {
                return $"http://api.openweathermap.org/data/2.5/{apiCall}?lat={lat}&lon={lon}&appid={appid}&units={units}";
            }
            else
            {
                return $"http://api.openweathermap.org/data/2.5/{apiCall}?lat={lat}&lon={lon}&exclude={exclude}&appid={appid}&units={units}";
            }
        }
    }
}