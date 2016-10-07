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
using SevenShadow.Finance.Core;

namespace SevenShadow.Quandl.DataSets
{
    public class SF1 : BaseDataSet
    {
        const string  DATASET_PREFIX = "SF1/";

        public enum DataItem
        {
            [Description("_SHARESBAS")]
            CommonStockSharesOutstanding = 1,

            [Description("_DPS")]
            Dividend = 2,

            [Description("_EQUITY")]
            Equity = 3,

            [Description("_NETINC")]
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

        public static IList<IBalanceSheet> GetHistoricalBalanceSheets(string tickerSymbol)
        {
            List<IBalanceSheet> sheets = new List<IBalanceSheet>();

            //switching to Quandl
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings.Add("trim_start", "2013-01-01");
            QuandlResponse response = Quandl.DataSets.SF1.GetDateValues(tickerSymbol.ToUpper(), Quandl.DataSets.SF1.DataItem.Equity, new Dictionary<string, string>(), SF1.Dimension.AsReportedQuarter);

            


            return sheets;

        }

        public static IFinancialStatementCollection GetHistoricalStatements(string tickerSymbol)
        {
            FinancialStatementCollectionBase col = new FinancialStatementCollectionBase();
            

            col.Statements = new List<IFinancialStatement>();

            //get balance sheets
            IList<IBalanceSheet> balanceSheets = GetHistoricalBalanceSheets(tickerSymbol);

         

            return col;
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
