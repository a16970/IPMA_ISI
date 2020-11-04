using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IPMA_2
{
    [JsonObject]
    public class City
    {
        public string owner { get; set; }
        public string country { get; set; }
        public List<Data> data { get; set; }
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
        }

        public City() { }

        public void AddLocal(string local)
        {
            this.local = local;
        }
    }
}
