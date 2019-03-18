using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestProject
{
   public class RestClientHelper
    {
        private readonly string apiBaseURL;

        private readonly RestClient restClient;

        public RestClientHelper(string apiBaseURL)
        {
            this.apiBaseURL = apiBaseURL;
            this.restClient = new RestClient(apiBaseURL);
        }

        public IRestResponse Get(string url)
        {
            var request = new RestRequest(url, Method.GET);
            return this.restClient.Execute(request); 
        }
    }
}
