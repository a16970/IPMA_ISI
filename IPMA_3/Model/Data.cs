using System.Globalization;
using Newtonsoft.Json;

namespace IPMA_3.Model
{
    public class Data
    {
        public string precipitaProb { get; set; }
        public Temperature temperature { get; set; }
        public string predWindDir { get; set; }
        public int idWeatherType { get; set; }
        public int classWindSpeed { get; set; }
        public string longitude { get; set; }
        public string forecastDate { get; set; }
        public int classPrecInt { get; set; }
        public string latitude { get; set; }

        [JsonConstructor]
        public Data(string precipitaProb, string tMin, string tMax, string predWindDir, int idWeatherType,
            int classWindSpeed, string longitude, string forecastDate, int classPrecInt, string latitude)
        {
            this.precipitaProb = precipitaProb;
            temperature = new Temperature(float.Parse(tMin, CultureInfo.InvariantCulture.NumberFormat), float.Parse(tMax, CultureInfo.InvariantCulture.NumberFormat));
            this.predWindDir = predWindDir;
            this.idWeatherType = idWeatherType;
            this.classWindSpeed = classWindSpeed;
            this.longitude = longitude;
            this.forecastDate = forecastDate;
            this.classPrecInt = classPrecInt;
            this.latitude = latitude;
        }

        public Data() { }
    }
}