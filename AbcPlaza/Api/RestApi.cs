using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace AbcPlaza.Api
{
    public class RestApi
    {
        //https://github.com/xamarin/xamarin-forms-samples/blob/master/WebServices/TodoREST/TodoREST/Data/RestService.cs

        private HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var uri = new Uri(url);
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, Object item)
        {

            var uri = new Uri(url);
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(uri, content);
        }


        public async Task<HttpResponseMessage> PutAsync(string url, Object item)
        {
            var uri = new Uri(url);
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PutAsync(uri, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var uri = new Uri(url);
            return await client.DeleteAsync(uri);
        }


    }
}