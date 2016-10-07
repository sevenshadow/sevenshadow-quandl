using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Net;
using SevenShadow.Quandl.HttpUtils;
using System.Configuration;

namespace SevenShadow.Quandl
{
    public class QuandlData
    {
        public string AuthToken = ConfigurationManager.AppSettings["QuandlAuthToken"].ToString();
        private const string QUANDL_API_URL = "https://www.quandl.com/api/v1/";
       
        public static string GetRawData(string dataset, IDictionary<string, string> settings, string format = "json")
        {
            HttpUtils.QuandlHelper myQuandl = new HttpUtils.QuandlHelper(ConfigurationManager.AppSettings["QuandlAuthToken"].ToString());
            string rawData = myQuandl.GetRawData(dataset, settings, format);
            return rawData;
        }

        public static string GetRawData(string requestUrl)
        {
            requestUrl = QUANDL_API_URL + requestUrl + "&auth_token=" + ConfigurationManager.AppSettings["QuandlAuthToken"].ToString();
            
            WebClient client = new WebClient();
            string rawData = client.DownloadString(requestUrl);

            return rawData;

        }

        
    }

}

 