using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenShadow.Quandl;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SevenShadow.Quandl.DataSets
{
    public class DMDRN : BaseDataSet
    {
        const string DATASET_PREFIX = "DMDRN/";

        public enum DataItem
        {
            [Description("_DIV")]
            Dividend = 1,

            [Description("_BV_EQTY")]
            BookValueOfEquity = 2,
            
            [Description("_FLOAT")]
            CommonSharesOutstanding = 3,

            [Description("_NET_INC")]
            NetIncome = 4,

          

        }

        public enum Dimension
        {
            [Description("")]
            NoDimension = 1,

            [Description("_ARY")]
            AsReportedYear = 2,

            [Description("_ARQ")]
            AsReportedQuarter = 3,

            [Description("_ART")]
            AsReportedTrailingTwelveMonths = 4,

            [Description("_MRY")]
            MostRecentYear = 5,


            [Description("_MRQ")]
            MostRecentQuarter = 6,

            [Description("_MRT")]
            MostRecentTrailingTwelveMonths = 7,



        }

        public static QuandlResponse GetDateValues(string tickerSymbol, DataItem dataItem, IDictionary<string, string> settings, Dimension dimension, string format = "json")
        {
            string dataset = DATASET_PREFIX + tickerSymbol + GetEnumDescription(dataItem) + GetEnumDescription(dimension);

            HttpUtils.QuandlHelper myQuandl = new HttpUtils.QuandlHelper();

            string rawData = QuandlData.GetRawData(dataset, settings, format);

            QuandlResponse filingResponse = (QuandlResponse)JsonConvert.DeserializeObject(rawData, typeof(QuandlResponse));

            JObject jsonObject = JObject.Parse(rawData);
            filingResponse.DateValues = new List<QuandlDateValue>();
            for (int i = 0; i < jsonObject["data"].Count(); i++)
            {
                filingResponse.DateValues.Add(new QuandlDateValue() { QuandlDate = DateTime.Parse(jsonObject["data"][i][0].ToString()), Value = decimal.Parse(jsonObject["data"][i][1].ToString()) });
            }
            return filingResponse;

        }

        public static QuandlResponse GetQuarterlyDividends(string[] tickerSymbols,IDictionary<string, string> settings, string format = "json")
        {
          //  string tickerSymbol = "AAPL";

            string dataset = DATASET_PREFIX + GetEnumDescription(DataItem.Dividend) + tickerSymbols[0];

            HttpUtils.QuandlHelper myQuandl = new HttpUtils.QuandlHelper();

            string rawData = QuandlData.GetRawData(dataset, settings, format);

            QuandlResponse filingResponse = (QuandlResponse)JsonConvert.DeserializeObject(rawData, typeof(QuandlResponse));

            JObject jsonObject = JObject.Parse(rawData);
            filingResponse.DateValues = new List<QuandlDateValue>();
            for (int i = 0; i < jsonObject["data"].Count(); i++)
            {
                decimal div;
                if (decimal.TryParse(jsonObject["data"][i][1].ToString(), out div))
                    filingResponse.DateValues.Add(new QuandlDateValue() { QuandlDate = DateTime.Parse(jsonObject["data"][i][0].ToString()), Value = div });
                else
                    filingResponse.DateValues.Add(new QuandlDateValue() { QuandlDate = DateTime.Parse(jsonObject["data"][i][0].ToString()), Value = 0 });
            }
            return filingResponse;

        }
        
        public static IList<QuandlDateValue> GetDateValues(string tickerSymbol, DataItem dataItem,  IDictionary<string, string> settings, string format = "json")
        {
            string dataset = DATASET_PREFIX + tickerSymbol + GetEnumDescription(dataItem);

            HttpUtils.QuandlHelper myQuandl = new HttpUtils.QuandlHelper();

            string rawData = QuandlData.GetRawData(dataset, settings, format);

            JObject jsonObject = JObject.Parse(rawData);

            List<QuandlDateValue> dateValues = new List<QuandlDateValue>();
            for (int i = 0; i < jsonObject["data"].Count(); i++)
            {
                dateValues.Add(new QuandlDateValue() { QuandlDate = DateTime.Parse(jsonObject["data"][i][0].ToString()), Value = decimal.Parse(jsonObject["data"][i][1].ToString()) });
            }

            return dateValues;
        }

       
       
            
    }
}
