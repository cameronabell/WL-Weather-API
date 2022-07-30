﻿namespace wild_lou_weather_api.Models
{
    public class ForecastResponse
    {
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
        public DateTime timestampUTC { get; set; }
    }
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Metrics main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        public System sys { get; set; }
        public string dt_txt { get; set; }
    }
    public class System
    {
        public string pod { get; set; }
    }


}
