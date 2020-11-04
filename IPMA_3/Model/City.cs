using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IPMA_3.Model
{
    [JsonObject]
    public class City
    {
        public string owner { get; set; }
        public string country { get; set; }
        public List<Data> data { get; set; }
        public Temperature amplitude { get; set; }
        public int globalIdLocal { get; set; }
        public string dataUpdate { get; set; }
        public string local { get; set; }

        [JsonConstructor]
        public City(string owner, string country, IEnumerable<Data> data, int globalIdLocal, string dataUpdate)
        {
            this.owner = owner;
            this.country = country;
            this.data = new List<Data>(data);
            this.globalIdLocal = globalIdLocal;
            this.dataUpdate = dataUpdate;
            amplitude = new Temperature(this.data.Min(t => t.temperature.TMin), this.data.Max(t => t.temperature.TMax));
        }

        public City() { }

        public void AddLocal(string local)
        {
            this.local = local;
        }
    }
}
