# WL-Weather-API
Gets the latest weather for any given coordinates. Used by the WildernessLouisville.org website to display weather to its users.

## Why was this created?

Wilderness Louisville originally used a plugin that connected directly to the OpenWeatherMap API. However, the large amount of traffic to the website caused them to quickly reach the OpenWeatherMap API's free quota. So, instead of doing a call to OpenWeatherMap API each time a user navigates to a webpage, it now calls this API, greatly reducing the number of requests made to OpenWeatherMap.

## How it works
When calling any endpoints, the API will first attempt to get the weather from cache. If available, it will return what's in cache. If the cache is older than 2 minutes OR of no cache exists, it will make a request to the OpenWeatherMap API to get the latest data.

So now if multiple of users are accessing the Wilderness Louisville website at the same time, they will all be calling this API to get the weather from cache. Whenever the cache reaches 2 minutes old, it will make a single API request to OpenWeatherMap. The max requests this API will make to OpenWeatherMap is 21,900 per month, well under the 1,000,000 calls/month quota.

> Created By [Cameron Abell](https://cameronabell.com) - Last Updated 7/30/2022

----

# Endpoints Available
## Get Current Weather
Gets the current weather for the given geographic coordinates. More details can be found [here](https://openweathermap.org/current).

    GET /weather?units={units}&lang={lang}&lat={lat}&lon={lon}&appid={appid}

### Query Parameters
    units - Unit of measurement the returned data uses (imperial or metric)
    lang - Language the returned data uses
    appid - OpenWeatherMap API Key
    lon - Longitudinal Coordinate
    lat - Latitudinal Coordinate

### Example Response
```
{
    "coord": {
        "lat": 38.0779,
        "lon": -85.7601
    },
    "weather": [
        {
            "id": 800,
            "main": "Clear",
            "description": "clear sky",
            "icon": "01n"
        }
    ],
    "main": {
        "temp": 66.49,
        "feels_like": 66.69,
        "temp_min": 61.23,
        "temp_max": 69.01,
        "pressure": 1020,
        "sea_level": 0,
        "grnd_level": 0,
        "humidity": 82,
        "average_temp": 65.12
    },
    "visibility": 10000,
    "wind": {
        "speed": 0,
        "deg": 0,
        "gust": 0
    },
    "clouds": {
        "all": 0
    },
    "rain": null,
    "snow": null,
    "dt": 1659154519,
    "timestampUTC": "2022-07-30T04:15:19",
    "sys": {
        "type": 2,
        "id": 2004341,
        "message": null,
        "country": "US",
        "sunrise": 1659177846,
        "sunriseUTC": "2022-07-30T10:44:06",
        "sunset": 1659228880,
        "sunsetUTC": "2022-07-31T00:54:40"
    },
    "timezone": -14400,
    "id": 4291373,
    "name": "Fairdale",
    "cod": 200
}
```
## Get 5 Day / 3 hour Forecast
Gets the weather forcast for 5 days with data every 3 hours for the given geographic coordinates. More details can be found [here](https://openweathermap.org/forecast5).

    GET /forecast?units={units}&lang={lang}&lat={lat}&lon={lon}&appid={appid}
### Query Parameters
    units - Unit of measurement the returned data uses (imperial or metric)
    lang - Language the returned data uses
    appid - OpenWeatherMap API Key
    lon - Longitudinal Coordinate
    lat - Latitudinal Coordinate

### Example Response
```
{
  "cod": "200",
  "message": 0,
  "cnt": 40,
  "list": [
    {
      "dt": 1647345600,
      "main": {
        "temp": 286.88,
        "feels_like": 285.93,
        "temp_min": 286.74,
        "temp_max": 286.88,
        "pressure": 1021,
        "sea_level": 1021,
        "grnd_level": 1018,
        "humidity": 62,
        "temp_kf": 0.14
      },
      "weather": [
        {
          "id": 804,
          "main": "Clouds",
          "description": "overcast clouds",
          "icon": "04d"
        }
      ],
      "clouds": {
        "all": 85
      },
      "wind": {
        "speed": 3.25,
        "deg": 134,
        "gust": 4.45
      },
      "visibility": 10000,
      "pop": 0,
      "sys": {
        "pod": "d"
      },
      "dt_txt": "2022-03-15 12:00:00"
    },
    {
      "dt": 1647356400,
      "main": {
        "temp": 286.71,
        "feels_like": 285.77,
        "temp_min": 286.38,
        "temp_max": 286.71,
        "pressure": 1021,
        "sea_level": 1021,
        "grnd_level": 1017,
        "humidity": 63,
        "temp_kf": 0.33
      },
      "weather": [
        {
          "id": 804,
          "main": "Clouds",
          "description": "overcast clouds",
          "icon": "04d"
        }
      ],
      "clouds": {
        "all": 90
      },
      "wind": {
        "speed": 3.34,
        "deg": 172,
        "gust": 4.03
      },
      "visibility": 10000,
      "pop": 0,
      "sys": {
        "pod": "d"
      },
      "dt_txt": "2022-03-15 15:00:00"
    },

    ...

 ],
  "city": {
    "id": 2643743,
    "name": "London",
    "coord": {
      "lat": 51.5073,
      "lon": -0.1277
    },
    "country": "GB",
    "population": 1000000,
    "timezone": 0,
    "sunrise": 1647324903,
    "sunset": 1647367441
  }
}
```
## OpenWeatherMap One Call
**Important:** Must be subscribed and using a [OpenWeatherMap One Call API Key](https://openweathermap.org/price)

Gets the current weather, minute forecast for 1 hour, hourly forecast for 48 hours, daily forecast for 8 days and government weather alerts for the given geographic coordinates. More details can be found [here](https://openweathermap.org/api/one-call-3).

    GET /onecall?units={units}&lang={lang}&lat={lat}&lon={lon}&appid={appid}
### Query Parameters
    units - Unit of measurement the returned data uses (imperial or metric)
    lang - Language the returned data uses
    appid - OpenWeatherMap API Key
    lon - Longitudinal Coordinate
    lat - Latitudinal Coordinate