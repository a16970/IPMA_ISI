using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IPMA_2
{
    public class Data
    {
        public string precipitaProb { get; set; }
        public string tMin { get; set; }
        public string tMax { get; set; }
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
            this.tMin = tMin;
            this.tMax = tMax;
            this.predWindDir = predWindDir;
            this.idWeatherType = idWeatherType;
            this.classWindSpeed = classWindSpeed;
            this.longitude = longitude;
            this.forecastDate = forecastDate;
            this.classPrecInt = classPrecInt;
            this.latitude = latitude;
        }
        
        public Data () { }
    }
}
