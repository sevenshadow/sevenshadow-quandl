using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenShadow.Quandl.DataSets
{
    public class QuandlSentimentDateValue
    {
        public DateTime QuandlDate { get; set; }
        public decimal Bullish { get; set; }
        public decimal Bearish { get; set; }

    }
    public class Psych : BaseDataSet
    {
        const string DATASET_PREFIX = "PSYCH/";

        public enum DataItem
        {
            [Description("_I")]
            SentimentIndex = 1,
        }


        public static IList<QuandlSentimentDateValue> GetDateValues(string tickerSymbol, DataItem dataItem, IDictionary<string, string> settings, string format = "json")
        {
            string dataset = DATASET_PREFIX + tickerSymbol + GetEnumDescription(dataItem);

            HttpUtils.QuandlHelper myQuandl = new HttpUtils.QuandlHelper();

            string rawData = QuandlData.GetRawData(dataset, settings, format);

            JObject jsonObject = JObject.Parse(rawData);

            List<QuandlSentimentDateValue> dateValues = new List<QuandlSentimentDateValue>();
            for (int i = 0; i < jsonObject["data"].Count(); i++)
            {
                dateValues.Add(new QuandlSentimentDateValue() { QuandlDate = DateTime.Parse(jsonObject["data"][i][0].ToString()), Bullish = decimal.Parse(jsonObject["data"][i][1].ToString()),
                Bearish = decimal.Parse(jsonObject["data"][i][2].ToString())});
            }

            return dateValues;
        }
    }
}
