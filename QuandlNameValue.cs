using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenShadow.Quandl
{
    public class QuandlNameValue
    {
          [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
      [JsonProperty(PropertyName = "value")]
          public string value { get; set; }
    }
}
