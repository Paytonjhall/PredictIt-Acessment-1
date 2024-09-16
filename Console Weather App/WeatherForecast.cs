namespace PredictItSkillDemonstrator
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
        
        public string ToString()
        {
            return $"Date: {Date.ToString()} \nTemperature C: {TemperatureC} \nTemperatureF: {TemperatureF} \nSummary: {Summary}";
        }
    }
    

}
