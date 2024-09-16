using PredictItSkillDemonstrator;
using Newtonsoft.Json;

public class WeatherHelper
    {
        static HttpClient client = new HttpClient();
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        
        //QUESTION #1 - On the line below add a comment which describes in detail what is happening in the code below
        public IEnumerable<WeatherForecast> CreateWeatherForecasts()
        {
            // Creating a variable to reference an instance of the Random class. Allows *random* number generation.
            var rng = new Random();
            // Returns an Enumerable array of WeatherForecasts. 
            // Range will create a enumerable spanning the two ints (1,2,3,4,5)
            // Select will project each sequences value into index, allowing a new WeatherForecast object to be made.
            // Finally, .ToArray() creates an array and populates it with the WeatherForecasts objects.
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    // Set WeatherForecast.Date to a date, going a day forward for each sequence index.
                    Date = DateTime.Now.AddDays(rng.Next(0, 100)),
                    // Randomly set temperatureC to a value between -20 and 54 (lower limit is inclusive while upper limit is not).
                    TemperatureC = rng.Next(-20, 55),
                    // Selects a random summary from the read only array of summaries. 
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();
        }
        //END QUESTION #1
        
        
        //QUESTION #2 - Fill in this function
        /// <summary>
        /// Return the forecasts from forecastsForNextMonth ordered by date
        /// </summary>
        /// <param name="forecastsForNextMonth">A list of forecasts for the month</param>
        /// <param name="coldTempCutOffInFarenheit">The temperature below or equal to which is considered cold (in farenheit)</param>
        /// <returns></returns>
        public WeatherForecast[] GetColdForecasts(List<WeatherForecast> forecastsForNextMonth, int coldTempCutOffInFarenheit)
        {
            // Return all forecasts WHERE the temperature is below the cold cut off, order by forecast date. return as array.
            return forecastsForNextMonth
                .Where((forecast) => forecast.TemperatureF <= coldTempCutOffInFarenheit).OrderBy(forecast => forecast.Date).ToArray();
        }
        //END QUESTION #2

        //QUESTION #3 - Create another function with the same name below which gets the cold forecasts but defines cold as below 50 degrees
        /// <summary>
        /// Return the forecasts from forecastsForNextMonth ordered by date
        /// </summary>
        /// <param name="forecastsForNextMonth">A list of forecasts for the month</param>
        /// <returns></returns>
        public WeatherForecast[] GetColdForecasts(List<WeatherForecast> forecastsForNextMonth)
        {
            // Set the cold cut off to 50, call previously created function to reduce code duplication.
            const int coldCutOffFarenheit = 50;
            return GetColdForecasts(forecastsForNextMonth, coldCutOffFarenheit);
        }
        
        //END QUESTION #3

        //QUESTION #4 - Create a function which calls the Open-Meteo API https://https://open-meteo.com/ and returns the weather temperature forecast for tomorrow afternoon for our Provo office at 86N University Avenue

        public async Task<double?> TomorrowTempInProvo()
        {
            // Exact lat long of provo office
            const string provoOfficeLat = "40.2348291";
            const string provoOfficeLong = "-111.6582061";
            const int daysInTheFuture = 1;
            const int afternoon = 5 + 12; // Noon + 5 = 5PM
            double temperature = 0;
            try
            {
                // Tried to reduce as many magic numbers as possible. If this string was an internal API I would suggest saving it as an env variable to allow correct endpoints for different env's.
                string url = $"https://api.open-meteo.com/v1/forecast?latitude={provoOfficeLat}&longitude={provoOfficeLong}&hourly=temperature_2m&forecast_days={daysInTheFuture}";
                // Make GET call
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Read in response
                    string respBody = await response.Content.ReadAsStringAsync();
                    // Convert to API Body
                    WeatherAPI weatherAPIBody = JsonConvert.DeserializeObject<WeatherAPI>(respBody);
                    // Find temperature
                    temperature = Math.Floor(weatherAPIBody.Hourly.Temperature_2m[afternoon]);
                    return temperature;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while retrieving weather data from Mateo: {ex}");
            }
            return temperature;
        }
        //END QUESTION #4
    }
