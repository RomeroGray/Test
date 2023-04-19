using IRAT.Inject.Model.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace IRAT.Inject.Business
{
    public class _ResmarkEndpoints
    {
        public bool Production = true;
        public string ProductionURL = "https://app.resmarksystems.com/public/api/";
        public string SandboxURL = "https://sandbox.resmarksystems.com/public/api/";

        public string username = "andrae.lyttle@islandroutes.com"; //"romerro.gray@islandroutes.com"; //"andrae.lyttle@islandroutes.com";
        public string Pwd = "7501981d-42a1-48cf-af42-c8f418f81eaa";

        public string SettingURL
        {
            get
            {
                return Production == true ? ProductionURL : SandboxURL;
            }
        }

        public string GetCredential
        {
            get
            {
                var content = "{ \"username\": " + "\"" + username + "\",  \"apikey\": " + "\"" + Pwd + "\"}";
                return content;
            }
        }

        public string Authorization()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(SettingURL + "authenticate");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(GetCredential);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        public async Task<string> TransactionDateDetails(string daterange, string orderIds, string conIds, string prodnumber)
        {
            string[] array = null;
            string from = null, to = null;
            string token = Authorization();

            using (var httpClient = new HttpClient { BaseAddress = new Uri(SettingURL) })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Bearer " + token.Trim());
                if (!string.IsNullOrEmpty(daterange))
                {
                    array = daterange.Split(new string[1] { "-" }, 2, StringSplitOptions.RemoveEmptyEntries);
                    from = Convert.ToDateTime(array[0]).ToString("yyyy-MM-dd");
                    to = Convert.ToDateTime(array[1]).ToString("yyyy-MM-dd");
                }
                using (var response = await httpClient.GetAsync($"report/order/transactionDate?from={from}&to={to}"))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            //using (var response = await httpClient.GetAsync($"report/order/transactionDate?from={from}&to={to}&orderIds={orderIds}&confirmationIds={conIds}&productNumbers={prodnumber}"))
        }


        public async Task<string> GetProductsById(string id)
        {
            string token = Authorization();

            using (var httpClient = new HttpClient { BaseAddress = new Uri(SettingURL) })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Bearer " + token.Trim());
                using (var response = await httpClient.GetAsync("product/" + id))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public List<DataLookup> ReadJsonfile()
        {
            using (StreamReader r = new StreamReader(HostingEnvironment.MapPath($"/App_Data/Datajson.json")))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<DataLookup>>(json);
            }
        }

    }
}
