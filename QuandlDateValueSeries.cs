using SevenShadow.Quandl.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenShadow.Quandl
{
    public class QuandlDateValueSeries
    {
        public string TickerSymbol { get; set; }

        public IList<QuandlSentimentDateValue> Series { get; set; }
        
    }

   
}
