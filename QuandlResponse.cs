using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenShadow.Quandl
{
    public class QuandlResponse
    {
        public string id { get; set; }
        public string source_name { get; set; }
        public string source_code { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string urlize_name { get; set; }
        public string display_url { get; set; }
        public string description { get; set; }
        public DateTime updated_at { get; set; }
        public string frequency { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public List<QuandlDateValue> DateValues { get; set; }
        public List<QuandlEstimate> Estimates { get; set; }
        public bool premium { get; set; } 



    }
}
