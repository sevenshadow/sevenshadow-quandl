using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenShadow.Quandl
{
    public class QuandlDateValue
    {
        public DateTime QuandlDate { get; set; }
        public decimal Value { get; set; }
    }

    public class QuandlEstimate
    {
        public DateTime QuandlDate { get; set; }
        public decimal Mean { get; set; }
        public decimal Median { get; set; }
        public decimal Low { get; set; }
        public decimal High { get; set; }
        public decimal Count { get; set; }
        public decimal StandardDeviation { get; set; }
        public decimal PercentChange { get; set; }
        public int FiscalYear { get; set; }
        public int FiscalQuarter { get; set; }
        public int CalendarYear { get; set; }
        public int CalendarQuarter { get; set; }
        public int PerCode { get; set; }
        public decimal RevisionUp { get; set; }
        public decimal RevisionDown { get; set; }



    }
}
