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
    public class ZEE : BaseDataSet
    {
        const string DATASET_PREFIX = "ZEE/";
        const string MULTISET_PREFIX = "multisets";

       

        public static QuandlResponse GetDateValues(string tickerSymbol, IDictionary<string, string> settings, PeriodType periodType, string format = "json")
        {

            string dataset = DATASET_PREFIX + tickerSymbol + GetEnumDescription(periodType);

            HttpUtils.QuandlHelper myQuandl = new HttpUtils.QuandlHelper();

            string rawData = QuandlData.GetRawData(dataset, settings, format);

            QuandlResponse filingResponse = (QuandlResponse)JsonConvert.DeserializeObject(rawData, typeof(QuandlResponse));

            JObject jsonObject = JObject.Parse(rawData);
            filingResponse.Estimates = new List<QuandlEstimate>();

            for (int i = 0; i < jsonObject["data"].Count(); i++)
            {
                if (periodType == PeriodType.LongTermGrowthEstimate)
                {
                    filingResponse.Estimates.Add(new QuandlEstimate()
                    {
                        Mean = decimal.Parse(jsonObject["data"][i][4].ToString())
                    });
                } 
    
                else
                {
                    filingResponse.Estimates.Add(new QuandlEstimate()
                    {
                        QuandlDate = DateTime.Parse(jsonObject["data"][i][0].ToString()),
                        Mean = decimal.Parse(jsonObject["data"][i][1].ToString()),
                        Median = decimal.Parse(jsonObject["data"][i][2].ToString()),
                        High = decimal.Parse(jsonObject["data"][i][3].ToString()),
                        Low = decimal.Parse(jsonObject["data"][i][4].ToString()),
                        Count = decimal.Parse(jsonObject["data"][i][5].ToString()),
                        StandardDeviation = jsonObject["data"][i][6].ToString() != string.Empty ? decimal.Parse(jsonObject["data"][i][6].ToString()) : 0,
                        PercentChange = jsonObject["data"][i][7].ToString() != string.Empty ? decimal.Parse(jsonObject["data"][i][7].ToString()) : 0,
                        FiscalYear = int.Parse(jsonObject["data"][i][8].ToString()),
                        FiscalQuarter = int.Parse(jsonObject["data"][i][9].ToString()),
                        CalendarYear = int.Parse(jsonObject["data"][i][10].ToString()),

                        CalendarQuarter = int.Parse(jsonObject["data"][i][11].ToString()),
                        PerCode = jsonObject["data"][i][12].ToString() != string.Empty ? int.Parse(jsonObject["data"][i][12].ToString()) : 0

                    });
                }
            }
            return filingResponse;

        }

         
         public static QuandlResponse GetEstimatesMulit(string[] tickerSymbols, IDictionary<string, string> settings,PeriodType periodType, string format = "json")
        {
            string tickerString = string.Empty;

            
            for (int i = 0; i < tickerSymbols.Length; i++)
            {
                //for (int j = 1; j < 13; j++)
               // {
                int j = 1;

                    tickerString += "ZEE" + "." + tickerSymbols[i].ToUpper() + GetEnumDescription(periodType) + "." + j.ToString() + ",";
                    
                //}
                
            }
            //cut off the trailing ,
            tickerString = tickerString.TrimEnd(char.Parse(","));

            string querystring = MULTISET_PREFIX + "." + format + "?columns=" + tickerString;
            foreach (KeyValuePair<string, string> kvp in settings)
            {
                querystring += String.Format("&{0}={1}", kvp.Key, kvp.Value);
            }

            HttpUtils.QuandlHelper myQuandl = new HttpUtils.QuandlHelper();
            string rawData = QuandlData.GetRawData(querystring);

            QuandlResponse filingResponse = (QuandlResponse)JsonConvert.DeserializeObject(rawData, typeof(QuandlResponse));
            JObject jsonObject = JObject.Parse(rawData);
            
            filingResponse.Estimates = new List<QuandlEstimate>();


            for (int i = 0; i < jsonObject["data"].Count(); i++)
            {
               // for (int j = 0; j < tickerSymbols.Length; i++)
                //{
                  //  if ()
                    
                //}

                filingResponse.Estimates.Add(new QuandlEstimate() { 
                    QuandlDate = DateTime.Parse(jsonObject["data"][i][0].ToString()), 
                    Mean = decimal.Parse(jsonObject["data"][i][1].ToString()), 
                    Median = decimal.Parse(jsonObject["data"][i][2].ToString()) ,
                    High = decimal.Parse(jsonObject["data"][i][3].ToString()),
                    Low = decimal.Parse(jsonObject["data"][i][4].ToString()),
                    Count = decimal.Parse(jsonObject["data"][i][5].ToString()),
                    StandardDeviation = decimal.Parse(jsonObject["data"][i][6].ToString()),
                    PercentChange = decimal.Parse(jsonObject["data"][i][7].ToString()),
                    FiscalYear = int.Parse(jsonObject["data"][i][8].ToString()),
                    FiscalQuarter = int.Parse(jsonObject["data"][i][9].ToString()),
                    CalendarYear = int.Parse(jsonObject["data"][i][10].ToString()),

                    CalendarQuarter = int.Parse(jsonObject["data"][i][11].ToString()),
                    PerCode = int.Parse(jsonObject["data"][i][12].ToString()),

                });
            }
            return filingResponse;

        }

       
            
    }
}
