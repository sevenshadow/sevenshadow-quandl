using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenShadow.Quandl
{
    public class ResidualEarningsSeries
    {
        public string TickerSymbol { get; set; }

        public IList<ResidualEarningsSeriesItem> Series { get; set; }
        
    }

    public class ResidualEarningsSeriesItem
    {
        public QuandlDateValue BookValueOfEquity0 { get; set; }
        public QuandlDateValue Earnings1 { get; set; }
        public QuandlDateValue SharesOutstanding0 { get; set; }
        public decimal ResidualEarnings1
        {
            get
            {
                return Earnings1.Value - (BookValueOfEquity0.Value * RequiredReturn);
            }
        }

        public decimal Earnings1PerShare
        {
            get
            {
                return Earnings1.Value  / SharesOutstanding0.Value;
            }
        }

        public decimal ResidualEarnings1PerShare
        {
            get
            {
                return (Earnings1.Value - (BookValueOfEquity0.Value * RequiredReturn))/SharesOutstanding0.Value;
            }
        }

        public decimal BookValue0PerShare
        {
            get
            {
                return BookValueOfEquity0.Value / SharesOutstanding0.Value;
            }
        }
        public decimal RequiredReturn { get; set; }
    }
}
