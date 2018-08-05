using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainLogBO.Models
{
    public class Region
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "center")]
        public Center Center { get; set; }
        [JsonProperty(PropertyName = "radius")]
        public double Radius { get; set; }
    }
}
