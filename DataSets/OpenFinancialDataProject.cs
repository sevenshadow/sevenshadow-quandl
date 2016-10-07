using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenShadow.Quandl;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace SevenShadow.Quandl.DataSets
{
    public class OpenFinancialDataProject : BaseDataSet
    {
        const string  DATASET_PREFIX = "OFDP/DMDRN_";

        public enum DataItem
        {
            [Description("_BV_EQTY")]
            BookValueOfEquity = 1,

            [Description("_NET_INC")]
            NetIncome = 2,

            [Description("_FLOAT")]
            SharesOutstanding = 3,

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
