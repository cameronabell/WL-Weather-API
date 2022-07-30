using Newtonsoft.Json;

namespace wild_lou_weather_api.Models
{
    public class WeatherResponse
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public Metrics main { get; set; }
        /// <summary>
        /// Visibility, meter. The maximum value of the visibility is 10km
        /// </summary>
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public Rain rain { get; set; }
        public Snow snow { get; set; }
        /// <summary>
        /// Time of data calculation, unix, UTC
        /// </summary>
        public int dt { get; set; }
        public DateTime timestampUTC { get; set; }
        public Sys sys { get; set; }
        /// <summary>
        /// Shift in seconds from UTC
        /// </summary>
        public int timezone { get; set; }
        /// <summary>
        /// City ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// City name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Internal parameter
        /// </summary>
        public int cod { get; set; }
    }
    public class Coord
    {
        /// <summary>
        /// City geo location, latitude
        /// </summary>
        public float lat { get; set; }
        /// <summary>
        /// City geo location, longitude
        /// </summary>
        public float lon { get; set; }
    }
    public class Sys
    {
        /// <summary>
        /// Internal parameter
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// Internal parameter
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Internal parameter
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Country code (GB, JP etc.)
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// Sunrise time, unix, UTC
        /// </summary>
        public int sunrise { get; set; }
        public DateTime sunriseUTC { get; set; }
        /// <summary>
        /// Sunset time, unix, UTC
        /// </summary>
        public int sunset { get; set; }
        public DateTime sunsetUTC { get; set; }

    }
    public class Rain
    {
        /// <summary>
        /// Rain volume for the last 1 hour, mm
        /// </summary>
        [JsonProperty("1H")]
        public float rainAmount1hr { get; set; }
        /// <summary>
        /// Rain volume for the last 3 hours, mm
        /// </summary>
        [JsonProperty("3H")]
        public float rainAmount3hr { get; set; }
    }
    public class Snow
    {
        /// <summary>
        /// Snow volume for the last 1 hour, mm
        /// </summary>
        [JsonProperty("1H")]
        public float snowAmount1hr { get; set; }
        /// <summary>
        /// Snow volume for the last 3 hours, mm
        /// </summary>
        [JsonProperty("3H")]
        public float snowAmount3hr { get; set; }
    }
    public class Wind
    {
        /// <summary>
        /// Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
        /// </summary>
        public float speed { get; set; }
        /// <summary>
        /// Wind direction, degrees (meteorological)
        /// </summary>
        public int deg { get; set; }
        /// <summary>
        /// Wind gust. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour
        /// </summary>
        public float gust { get; set; }
    }
    public class Clouds
    {
        /// <summary>
        /// Cloudiness, %
        /// </summary>
        public int all { get; set; }
    }
    public class Metrics
    {
        /// <summary>
        /// Temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        public float temp { get; set; }
        /// <summary>
        /// Temperature. This temperature parameter accounts for the human perception of weather. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        public float feels_like { get; set; }
        /// <summary>
        /// Minimum temperature at the moment. This is minimal currently observed temperature (within large megalopolises and urban areas). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        public float temp_min { get; set; }
        /// <summary>
        /// Maximum temperature at the moment. This is maximal currently observed temperature (within large megalopolises and urban areas). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        public float temp_max { get; set; }
        /// <summary>
        /// Atmospheric pressure (on the sea level, if there is no sea_level or grnd_level data), hPa
        /// </summary>
        public float pressure { get; set; }
        /// <summary>
        /// Atmospheric pressure on the sea level, hPa
        /// </summary>
        public float sea_level { get; set; }
        /// <summary>
        /// Atmospheric pressure on the ground level, hPa
        /// </summary>
        public float grnd_level { get; set; }
        /// <summary>
        /// Humidity, %
        /// </summary>
        public float humidity { get; set; }
        public double average_temp { get { return Math.Round((temp_max + temp_min) / 2, 2); } }
    }
    public class Weather
    {
        /// <summary>
        /// Weather condition id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Group of weather parameters (Rain, Snow, Extreme etc.)
        /// </summary>
        public string main { get; set; }
        /// <summary>
        /// Weather condition within the group. You can get the output in your language.
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Weather icon id
        /// </summary>
        public string icon { get; set; }
    }
}
