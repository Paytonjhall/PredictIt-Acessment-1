public class WeatherAPI
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationtimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; }
    public string TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }
    public HourlyUnits HourlyUnits { get; set; }
    public Hourly Hourly { get; set; }
    
    public override string ToString()
    {
        return $"Latitude: {Latitude}, Longitude: {Longitude}, Elevation: {Elevation}m\n" +
               $"Timezone: {Timezone} ({TimezoneAbbreviation})\n" +
               $"Hourly Forecasts:\n{Hourly}";
    }
}

public class HourlyUnits
{
    public string Time { get; set; }
    public double temperature_2m { get; set; }
}

public class Hourly
{
    public List<string> Time { get; set; }
    public List<double> Temperature_2m { get; set; }

}
