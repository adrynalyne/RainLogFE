using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainLogBO.Models
{
    public class Result
    {
        [JsonProperty(PropertyName = "readingId")]
        public int ? ReadingId { get; set; }
        [JsonProperty(PropertyName = "gaugeId")]
        public int ? GaugeId { get; set; }
        [JsonProperty(PropertyName = "gaugeRevisionId")]
        public int ? GaugeRevisionId { get; set; }
        [JsonProperty(PropertyName = "remarks")]
        public string Remarks { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "readingDate")]
        public string ReadingDate { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "readingHour")]
        public int ? ReadingHour { get; set; }
        [JsonProperty(PropertyName = "readingMinute")]
        public int ? ReadingMinute { get; set; }
        [JsonProperty(PropertyName = "quality")]
        public string Quality { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "rainAmount")]
        public double ? RainAmount { get; set; }
        [JsonProperty(PropertyName = "snowDepth")]
        public object SnowDepth { get; set; } = null;
        [JsonProperty(PropertyName = "snowAccumulation")]
        public object SnowAccumulation { get; set; } = null;

    }
}
