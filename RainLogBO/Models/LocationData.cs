using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainLogBO.Models
{
    public class LocationData
    {
        [JsonProperty(PropertyName = "dateRangeStart")]
        public string DateRangeStart { get; set; }
        [JsonProperty(PropertyName = "dateRangeEnd")]
        public string DateRangeEnd { get; set; }
        [JsonProperty(PropertyName = "region")]
        public Region Region { get; set; }
    }
}
