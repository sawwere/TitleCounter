using hltb.Dto;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;

namespace hltb.Service
{
    internal class RestApiSerice
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static RestApiSerice restApiSerice;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private RestApiClient restApiClient;

        public static RestApiSerice Instance
        {
            get
            {
                if (restApiSerice == null)
                {
                    restApiSerice = new RestApiSerice();
                }
                return restApiSerice;
            }
        }
        private RestApiSerice() { 
            restApiClient = RestApiClient.Instance;
        }

        public Image GetImage(string imageUrl)
        {
            var response = restApiClient.HttpClient.GetStreamAsync($"{imageUrl}").Result;
            var img = Image.FromStream(response);
            return img;
        }
    }
}
