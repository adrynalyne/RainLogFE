using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RainLogBO.Helpers
{
    public class APIManager
    {
        public string Post(string requestUri, object postData)
        {

            try
            {
                var httClient = new HttpClient();

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
                return httClient.PostAsync(requestUri, content).Result.Content.ReadAsStringAsync().Result;
            }
            catch (OperationCanceledException oce)
            {
                throw oce;
            }



        }
    }
}
