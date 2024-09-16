// See https://aka.ms/new-console-template for more information

using PredictItSkillDemonstrator;

WeatherHelper weatherHelper = new WeatherHelper();
Console.WriteLine("Question 2 Response: ");
WeatherForecast[] coldForecasts = weatherHelper.GetColdForecasts(weatherHelper.CreateWeatherForecasts().ToList(), 100);
foreach (WeatherForecast forecast in coldForecasts)
{
 Console.WriteLine(forecast.ToString() + "\n");   
}
coldForecasts = weatherHelper.GetColdForecasts(weatherHelper.CreateWeatherForecasts().ToList());

Console.WriteLine("\n\nQuestion 3 Response: ");
foreach (WeatherForecast forecast in coldForecasts)
{
 Console.WriteLine(forecast.ToString() + "\n");   
}

Console.WriteLine("\n\nQuestion 4 Response: ");
Console.WriteLine("Weather tomorrow afternoon at the office will be " + await weatherHelper.TomorrowTempInProvo() + " degrees C.");
 