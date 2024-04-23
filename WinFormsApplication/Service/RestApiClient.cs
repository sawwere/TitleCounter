using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Service
{
    internal class RestApiClient
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static RestApiClient instance;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private HttpClient httpClient;
        private HttpClientHandler handler;

        public string JSESSIONID = "";

        public static RestApiClient Instance
        {
            get
            {
                if (instance == null)
                    instance = new RestApiClient();
                return instance;
            }
        }

        public HttpClient HttpClient { get { return httpClient; } }
        public HttpClientHandler Handler { get { return handler; } }

        private RestApiClient()
        {
            handler = new HttpClientHandler();
            CookieContainer cookies = new CookieContainer();
            handler.CookieContainer = cookies;
            httpClient = new HttpClient(this.handler);
            httpClient.BaseAddress = new Uri("http://localhost:8080/api");
        }
    }
}
